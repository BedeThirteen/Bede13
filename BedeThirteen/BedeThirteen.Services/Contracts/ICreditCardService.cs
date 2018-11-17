namespace BedeThirteen.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Models;

    public interface ICreditCardService
    {
        Task<CreditCard> AddCreditCardAsync(string number, string cvv, DateTime expiry, string userId);

        Task<ICollection<CreditCard>> GetUserCardsAsync(string id);
    }
}