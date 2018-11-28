namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BedeThirteen.Services.Contracts;
    using BedeThirteen.Services.Exceptions;
    using BedeThirteen.Services.Requesters;
    using BedeThirteen.Services.SerializationModels;
    using Newtonsoft.Json;

    public class ExchangeRateService : IExchangeRateService
    {
        private static readonly object LockObject = new object(); // single lock for both fields
        private readonly IExchangeRatesApiClient apiClient;
        private IDictionary<string, decimal> rates = new Dictionary<string, decimal>();

        private DateTime lastUpdate;

        public ExchangeRateService(IExchangeRatesApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IDictionary<string, decimal>> GetRatesAsync()
        {
            if (this.rates.Count == 0 || DateTime.UtcNow.Subtract(this.lastUpdate).Hours > 6)
            {
                ExchangeRates jsonResult = null;
                var address = "https://api.exchangeratesapi.io/latest?base=USD&symbols=EUR,USD,BGN,GBP";
                var response = await this.apiClient.GetResponse(address);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    jsonResult = JsonConvert.DeserializeObject<ExchangeRates>(responseBody);
                }

                if (jsonResult == null)
                {
                    // todo : inform user if api is unavailable
                    if (this.rates != null)
                    {
                        throw new ServiceException("Service unavailable!");
                    }

                    return this.rates;
                }

                lock (LockObject)
                {
                    this.rates = jsonResult.Rates;
                    this.lastUpdate = DateTime.UtcNow;
                }
            }

            return this.rates;
        }
    }
}
