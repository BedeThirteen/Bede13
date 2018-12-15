namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services.Contracts;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class CreditCardService : ICreditCardService
    {
        private readonly BedeThirteenContext context;

        public CreditCardService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<CreditCard> AddCreditCardAsync(string number, string cvv, DateTime expiry, string userId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(number) || string.IsNullOrEmpty(cvv) || expiry == null)
            {
                throw new ServiceException("Invalid parameters!");
            }

            var user = await this.context.Users
                .Include(u => u.CreditCards)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ServiceException($"No user found with Id {userId}.");
            }

            if (user.CreditCards.Any(cc => cc.Number == number))
            {
                throw new ServiceException($"User has already a card with number {number}!");
            }

            var card = new CreditCard() { Number = number, Cvv = cvv, Expiry = expiry, UserId = userId };
            await this.context.CreditCards.AddAsync(card);
            await this.context.SaveChangesAsync();

            return card;
        }

        public async Task<ICollection<CreditCard>> GetUserCardsAsync(string id)
        {
            if (await this.context.Users.FindAsync(id) == null)
            {
                throw new ServiceException($"No user found with Id {id}.");
            }

            return await this.context.CreditCards.Where(c => c.UserId == id).ToListAsync();
        }
    }
}