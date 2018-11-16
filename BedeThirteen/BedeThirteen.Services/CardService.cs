﻿namespace BedeThirteen.Services
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

    public class CardService : ICardService
    {
        private readonly BedeThirteenContext context;

        public CardService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<CreditCard> AddCreditCardAsync(string number, string cvv, DateTime expiry, string userId)
        {
            // TODO: can be moved to separate validator class
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(number) || string.IsNullOrEmpty(cvv) || expiry == null)
            {
                throw new ServiceException("Invalid parameters!");
            }

            if (await this.context.Users.FindAsync(userId) == null)
            {
                throw new ServiceException($"No user found with Id {userId}.");
            }

            var card = new CreditCard() { Number = number, Cvv = cvv, Expiry = expiry, UserId = userId };
            await this.context.CreditCards.AddAsync(card);
            await this.context.SaveChangesAsync();

            return card;
        }

        public async Task<ICollection<CreditCard>> GetUserCardsAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ServiceException("Invalid parameter: userId.");
            }

            if (await this.context.Users.FindAsync(id) == null)
            {
                throw new ServiceException($"No user found with Id {id}.");
            }

            return await this.context.CreditCards.Where(c => c.UserId == id).ToListAsync();
        }
    }
}