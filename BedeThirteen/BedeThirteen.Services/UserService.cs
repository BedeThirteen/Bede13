namespace BedeThirteen.Services
{
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services.Contracts;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly BedeThirteenContext context;

        public UserService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserAsync(string userId)
        {
            return await this.context.Users.Include(u => u.Currency)
                             .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<string> GetUserBalanceAsync(string userId)
        {
            var user = await this.context.Users.Include(u => u.Currency).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ServiceException("Invalid userId!");
            }

            return string.Concat(user.Balance, " ", user.Currency.Name);
        }
    }
}
