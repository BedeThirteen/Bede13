namespace BedeThirteen.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Models;

    public interface ICurrencyService
    {
        Task<bool> CurrencyIsValidAsync(string givenNotation);

        Task<Currency> GetCurrencyByNameAsync(string name);

        Task<Currency> GetCurrencyAsync(Guid currencyId);

        Task<IList<Currency>> GetAllCurrenciesAsync();
    }
}