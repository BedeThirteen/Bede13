namespace BedeThirteen.Services.Contracts
{
    using System.Threading.Tasks;
    using BedeThirteen.Data.Models;

    public interface IUserService
    {
        Task<User> GetUserAsync(string userId);

        Task<string> GetUserBalanceAsync(string userId);
    }
}