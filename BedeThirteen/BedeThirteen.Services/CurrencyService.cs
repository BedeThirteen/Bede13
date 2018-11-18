using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BedeThirteen.Services
{
  public  class CurrencyService : ICurrencyService
    {
        private readonly BedeThirteenContext context;

        public CurrencyService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<Currency> GetCurrencyByNameAsync(string name)
        {
            return await this.context.Currencies.FirstAsync(currency => currency.Name == name);
        }

        public async Task<bool> CurrencyIsValidAsync(string givenNotation)
        { 
            return await this.context.Currencies.AnyAsync(currency => currency.Name == givenNotation);
        }

    }
}
