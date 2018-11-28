namespace BedeThirteen.Services.Requesters
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ExchangeRatesApiClient : IExchangeRatesApiClient
    {
        private readonly HttpClient client;

        public ExchangeRatesApiClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<HttpResponseMessage> GetResponse(string address)
        {
            return await this.client.GetAsync(address);
        }
    }
}
