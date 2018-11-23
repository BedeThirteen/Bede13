namespace BedeThirteen.Services.Contracts
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<string> GetUserBalance(string userId);
    }
}