namespace BedeThirteen.Services.Requesters
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IExchangeRatesApiClient
    {
        Task<HttpResponseMessage> GetResponse(string adress);
    }
}