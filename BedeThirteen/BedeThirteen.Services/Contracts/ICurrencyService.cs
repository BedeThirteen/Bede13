using System.Threading.Tasks;
using BedeThirteen.Data.Models;

namespace BedeThirteen.Services.Contracts
{
   public interface ICurrencyService
    {
        Task<bool> CurrencyIsValidAsync(string givenNotation);

        Task<Currency> GetCurrencyByNameAsync(string name);
    }
}