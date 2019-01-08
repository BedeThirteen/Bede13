namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services.CompositeModels;
    using BedeThirteen.Services.Contracts;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class TransactionService : ITransactionService
    {
        private readonly BedeThirteenContext context;

        public TransactionService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<TransactionsResult> GetTransactionsAsync(
            string filterBy,
            string filterCriteria,
            string aditionalCriteria,
            int pageSize,
            int pageNumber,
            string sortBy,
            int archiveKey,
            string userId)
        {
            var sortDictionary = new Dictionary<string, Expression<Func<Transaction, object>>>()
            {
                { "date", t => t.Date },
                { "date_desc", t => t.Date },
                { "amount", t => t.Amount },
                { "amount_desc", t => t.Amount }
            };

            var transactions = this.context.Transactions.AsQueryable();
            transactions = archiveKey == 0
                            ? transactions.Where(t => t.IsDeleted == false)
                            : transactions.Where(t => t.IsDeleted == true);

            if (userId != string.Empty)
            {
                transactions = transactions.Where(t => t.UserId == userId);
            }

            // filter
            if (filterBy != "all")
            {
                var filterByDictionary = new Dictionary<string, Expression<Func<Transaction, bool>>>()
                {
                    { "date", t => t.Date.Date == this.TryParseDate(filterCriteria).Date },
                    {
                      "dateAditional", t => t.Date.Date >= this.TryParseDate(filterCriteria)
                                         && t.Date.Date <= this.TryParseDate(aditionalCriteria).Date
                    },
                    { "amount", t => t.Amount == this.TryParseAmount(filterCriteria) },
                    {
                      "amountAditional", t => t.Amount >= this.TryParseAmount(filterCriteria)
                                         && t.Amount <= this.TryParseAmount(aditionalCriteria)
                    },
                    { "type", t => t.TransactionType.Name == filterCriteria },
                    { "user", t => t.User.UserName.Contains(filterCriteria) }
                };

                if (!string.IsNullOrEmpty(aditionalCriteria))
                {
                    filterBy += "Aditional";
                }

                var filter = filterByDictionary[filterBy];
                transactions = transactions.Where(filter);
            }

            // totalCount
            var count = transactions.Count();

            // sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                transactions = sortBy.Contains("desc")
                ? transactions.OrderByDescending(sortDictionary[sortBy])
                : transactions.OrderBy(sortDictionary[sortBy]);
            }

            if (userId == string.Empty)
            {
                transactions = transactions.Include(t => t.User);
            }

            // paging
            var result = await transactions
                .Include(t => t.TransactionType)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .AsNoTracking().ToListAsync();

            return new TransactionsResult() { Transactions = result, TotalCount = count };
        }


        private async Task<decimal> AddBalanceTargetedTransaction(string userId, decimal changeInBalance, Guid cardId, TransactionType type)
        {

            if (string.IsNullOrEmpty(userId) || changeInBalance == 0 || string.IsNullOrEmpty(cardId.ToString()))
            {
                throw new ServiceException("Invalid parameters!");
            }

            var user = await this.context.Users.FindAsync(userId);

            var card = await this.context.CreditCards.FirstOrDefaultAsync(c => c.Id == cardId && c.UserId == userId);
            if (card == null)
            {
                throw new ServiceException($"Invalid credit card!");
            }

            if (user.Balance + changeInBalance < 0)
            {
                throw new ServiceException("Invalid amount!");
            }
            var deposit = new Transaction()
            {
                Date = DateTime.UtcNow,
                Amount = Math.Abs(changeInBalance),
                Description = $"{type.Name} with card {this.Mask(card.Number)}",
                UserId = userId,
                TransactionTypeId = type.Id
            };

            this.context.Transactions.Add(deposit);

            user.Balance += changeInBalance;

            await this.context.SaveChangesAsync();

            return user.Balance;
        }


        public async Task<decimal> DepositAsync(string userId, decimal amount, Guid cardId)
        {
            if (amount <= 0)
            {
                throw new ServiceException("Invalid parameters!");
            }

            var type = await this.context.TransactionTypes.FirstOrDefaultAsync(tt => tt.Name == "Deposit");
            return await this.AddBalanceTargetedTransaction(userId, amount, cardId,type);
        }

        public async Task<decimal> WithdrawAsync(string userId, decimal amount, Guid cardId)
        {
            if (amount <= 0)
            {
                throw new ServiceException("Invalid parameters!");
            }

            var type = await this.context.TransactionTypes.FirstOrDefaultAsync(tt => tt.Name == "Withdraw");
            return await this.AddBalanceTargetedTransaction(userId, -1 * amount, cardId, type);
        }


        private async Task<decimal> AddGameTargetedTransaction(string userId, decimal amount,
            string gameName, TransactionType type)
        {
            if (string.IsNullOrEmpty(userId) || amount == 0)
            {
                throw new ServiceException("Invalid parameters!");
            }

            var user = await this.context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new ServiceException("User not found.");
            }

            if (user.Balance + amount < 0)
            {
                throw new ServiceException("Invalid amount!");
            }

            var stake = new Transaction()
            {
                Date = DateTime.UtcNow,
                Amount = amount,
                Description = $"{type.Name} on game {gameName}",
                UserId = userId,
                TransactionTypeId = type.Id
            };

            this.context.Transactions.Add(stake);
            user.Balance += amount;

            await this.context.SaveChangesAsync();

            return user.Balance;
        }

            public async Task<decimal> StakeAsync(string userId, decimal amount, string gameName)
        {
            if(amount <= 0)
            {
                throw new ServiceException("Invalid parameters!");
            }

            var type = await this.context.TransactionTypes.FirstOrDefaultAsync(tt => tt.Name == "Stake");

            return await this.AddGameTargetedTransaction(userId, -1 * amount, gameName, type);
        }

        public async Task<decimal> WinAsync(string userId, decimal amount, string gameName)
        {
            if (amount <= 0)
            {
                throw new ServiceException("Invalid parameters!");
            }

            var type = await this.context.TransactionTypes.FirstOrDefaultAsync(tt => tt.Name == "Win");
            return await this.AddGameTargetedTransaction(userId, amount, gameName, type);
        }

        public async Task<int> ArchiveTransactionsAsync(DateTime dateFrom, DateTime dateTo)
        {
            var transactions = await this.context.Transactions.Where(
              t => t.Date.Date >= dateFrom.Date
              && t.Date.Date <= dateTo.Date && t.IsDeleted == false)
              .ToListAsync();

            if (transactions.Count == 0)
            {
                throw new ServiceException("No transactions found!");
            }

            foreach (var transaction in transactions)
            {
                transaction.IsDeleted = true;
                transaction.DeletedOn = DateTime.UtcNow;
            }

            await this.context.SaveChangesAsync();
            return transactions.Count;
        }

        private string Mask(string number)
        {
            return new string('\u2022', number.Length - 4) + number.Substring(number.Length - 4);
        }

        private DateTime TryParseDate(string filterCriteria)
        {
            if (DateTime.TryParse(filterCriteria, out DateTime temp))
            {
                return temp;
            }

            throw new ServiceException("Invalid Date format.");
        }

        private decimal TryParseAmount(string filterCriteria)
        {
            if (decimal.TryParse(filterCriteria, out decimal temp))
            {
                return temp;
            }

            throw new ServiceException("Invalid Amount format.");
        }
    }
}
