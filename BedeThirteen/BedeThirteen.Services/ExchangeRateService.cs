namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BedeThirteen.Services.Contracts;
    using BedeThirteen.Services.Exceptions;
    using BedeThirteen.Services.SerializationModels;
    using Newtonsoft.Json;

    public class ExchangeRateService : IExchangeRateService
    {
        private static readonly object LockObject = new object(); // single lock for both fields

        private IDictionary<string, decimal> rates = new Dictionary<string, decimal>();

        private DateTime lastUpdate;

        public async Task<IDictionary<string, decimal>> GetRatesAsync()
        {
            if (this.rates.Count == 0 || DateTime.UtcNow.Subtract(this.lastUpdate).Hours > 6)
            {
                ExchangeRates jsonResult = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.exchangeratesapi.io");

                    var response = await client.GetAsync("/latest?base=USD&symbols=EUR,USD,BGN,GBP");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        jsonResult = JsonConvert.DeserializeObject<ExchangeRates>(responseBody);
                    }
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
