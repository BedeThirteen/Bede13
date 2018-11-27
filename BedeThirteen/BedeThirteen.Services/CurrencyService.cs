﻿namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class CurrencyService : ICurrencyService
    {
        private readonly BedeThirteenContext context;

        public CurrencyService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<Currency> FindCurrencyAsync(Guid currencyId)
        {
            var currency = await this.context.Currencies.FindAsync(currencyId);
            return currency;
        }

        public async Task<Currency> GetCurrencyByNameAsync(string name)
        {
            return await this.context.Currencies.FirstAsync(currency => currency.Name == name);
        }

        public async Task<IList<Currency>> GetAllCurrenciesAsync()
        {
            return await this.context.Currencies.Where(c => c.Name != "none").ToListAsync();
        }

        public async Task<bool> CurrencyIsValidAsync(string givenNotation)
        {
            return await this.context.Currencies.AnyAsync(currency =>
            this.CurrencyStringComparer(currency.Name.ToUpper(), givenNotation.ToUpper()));
        }

        private bool CurrencyStringComparer(string firstCurrency, string secondCurrency)
        {
            return firstCurrency.ToUpper() == secondCurrency.ToUpper();
        }
    }
}
