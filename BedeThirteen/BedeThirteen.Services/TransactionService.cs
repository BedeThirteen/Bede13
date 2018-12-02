namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class TransactionService : ITransactionService
    {
        private readonly BedeThirteenContext context;

        public TransactionService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Transaction>> GetLastNTransactions(int pageLength, int pagesToSkip = 0)
        {
            return await this.context.Transactions.Include(t => t.TransactionType)
                 .Include(t => t.User).OrderByDescending(t => t.Date)
                 .Skip(pagesToSkip * pageLength).Take(pageLength)
                 .ToListAsync();
        }

        public async Task<decimal> DepositAsync(string userId, decimal amount, Guid cardId)
        {
            if (string.IsNullOrEmpty(userId) || amount <= 0 || string.IsNullOrEmpty(cardId.ToString()))
            {
                throw new ServiceException("Invalid parameters!");
            }

            var user = await this.context.Users.FindAsync(userId);

            var card = await this.context.CreditCards.FirstOrDefaultAsync(c => c.Id == cardId && c.UserId == userId);
            if (card == null)
            {
                throw new ServiceException($"Invalid credit card!");
            }

            var type = await this.context.TransactionTypes.FirstOrDefaultAsync(tt => tt.Name == "Deposit");

            var deposit = new Transaction()
            {
                Date = DateTime.UtcNow,
                Amount = amount,
                Description = $"Deposit with card {card.Number}",
                UserId = userId,
                TransactionTypeId = type.Id
            };

            this.context.Transactions.Add(deposit);
            user.Balance += deposit.Amount;

            await this.context.SaveChangesAsync();

            return user.Balance;
        }

        public async Task<decimal> WithdrawAsync(string userId, decimal amount, Guid cardId)
        {
            if (string.IsNullOrEmpty(userId) || amount <= 0 || string.IsNullOrEmpty(cardId.ToString()))
            {
                throw new ServiceException("Invalid parameters!");
            }

            var user = await this.context.Users.FindAsync(userId);
            if (user.Balance < amount)
            {
                throw new ServiceException("Invalid amount!");
            }

            var card = await this.context.CreditCards.FirstOrDefaultAsync(c => c.Id == cardId && c.UserId == userId);
            if (card == null)
            {
                throw new ServiceException($"Invalid credit card!");
            }

            var type = await this.context.TransactionTypes.FirstOrDefaultAsync(tt => tt.Name == "Withdraw");

            var withdraw = new Transaction()
            {
                Date = DateTime.UtcNow,
                Amount = amount,
                Description = $"Withdraw to card {card.Number}",
                UserId = userId,
                TransactionTypeId = type.Id
            };

            this.context.Transactions.Add(withdraw);
            user.Balance -= withdraw.Amount;

            await this.context.SaveChangesAsync();

            return user.Balance;
        }

        public async Task<decimal> StakeAsync(string userId, decimal amount, string gameName)
        {
            if (string.IsNullOrEmpty(userId) || amount <= 0)
            {
                throw new ServiceException("Invalid parameters!");
            }

            var user = await this.context.Users.FindAsync(userId);
            if (user.Balance < amount)
            {
                throw new ServiceException("Invalid amount!");
            }

            var type = await this.context.TransactionTypes.FirstOrDefaultAsync(tt => tt.Name == "Stake");

            var stake = new Transaction()
            {
                Date = DateTime.UtcNow,
                Amount = amount,
                Description = $"Stake on game {gameName}",
                UserId = userId,
                TransactionTypeId = type.Id
            };

            this.context.Transactions.Add(stake);
            user.Balance -= stake.Amount;

            await this.context.SaveChangesAsync();

            return user.Balance;
        }

        public async Task<decimal> WinAsync(string userId, decimal amount, string gameName)
        {
            if (string.IsNullOrEmpty(userId) || amount <= 0)
            {
                throw new ServiceException("Invalid parameters!");
            }

            var user = await this.context.Users.FindAsync(userId);

            var type = await this.context.TransactionTypes.FirstOrDefaultAsync(tt => tt.Name == "Win");

            var win = new Transaction()
            {
                Date = DateTime.UtcNow,
                Amount = amount,
                Description = $"Win on game {gameName}",
                UserId = userId,
                TransactionTypeId = type.Id
            };

            this.context.Transactions.Add(win);
            user.Balance += win.Amount;

            await this.context.SaveChangesAsync();

            return user.Balance;
        }
    }
}
