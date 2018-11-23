namespace BedeThirteen.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExchangeRateService
    {
        Task<IDictionary<string, decimal>> GetRatesAsync();
    }
}