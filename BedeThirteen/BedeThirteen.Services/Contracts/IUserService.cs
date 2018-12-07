namespace BedeThirteen.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Models;

    public interface IUserService
    {
        Task<User> GetUserAsync(string userId);

        Task<string> GetUserBalanceWithCurrencyAsync(string userId);

        Task<IEnumerable<string>> GetAllEmailsAsync();

        Task<User> PromoteUserAsync(string email);
    }
}