namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BedeThirteen.Services.Exceptions;
    using BedeThirteen.Services.SerializationModels;
    using Newtonsoft.Json;

    public class ExchangeRateService
    {
        public IDictionary<string, decimal> Rates { get; private set; } = new Dictionary<string, decimal>();

        public DateTime LastUpdate { get; private set; }

        private static object lockObject = new object(); // single lock for both fields

        public async Task<IDictionary<string, decimal>> GetRates()
        {
            if (this.Rates.Count == 0 || DateTime.UtcNow.Subtract(this.LastUpdate).Hours > 6)
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
                    if (this.Rates != null)
                    {
                        throw new ServiceException("Service unavailable!");
                    }

                    return this.Rates;
                }

                lock (lockObject)
                {
                    this.Rates = jsonResult.Rates;
                    this.LastUpdate = DateTime.UtcNow;
                }
            }

            return this.Rates;
        }
    }
}
