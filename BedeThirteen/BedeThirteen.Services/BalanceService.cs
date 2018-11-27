namespace BedeThirteen.Services
{
    using System;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services.Contracts;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class BalanceService : IBalanceService
    {
        private readonly BedeThirteenContext context;

        public BalanceService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<string> GetUserBalance(string userId)
        {
            var user = await this.context.Users.Include(u => u.Currency).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ServiceException("Invalid userId!");
            }

            return string.Concat(user.Balance, " ", user.Currency.Name);
        }

        public async Task<decimal> Deposit(string userId, decimal amount, Guid cardId)
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
            if (type == null)
            {
                throw new ServiceException($"Cannot find transaction type : Deposit.");
            }

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
    }
}
