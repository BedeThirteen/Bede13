namespace BedeThirteen.Services
{
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services.Contracts;
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
    }
}
