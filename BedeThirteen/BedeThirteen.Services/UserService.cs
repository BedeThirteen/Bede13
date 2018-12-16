namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services.Contracts;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly BedeThirteenContext context;
        private readonly UserManager<User> userManager;

        public UserService(BedeThirteenContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<User> GetUserAsync(string userId)
        {
            return await this.context.Users.Include(u => u.Currency)
                             .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<string> GetUserBalanceWithCurrencyAsync(string userId)
        {
            var user = await this.context.Users.Include(u => u.Currency).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ServiceException("Invalid userId!");
            }

            return string.Concat(user.Balance, " ", user.Currency.Name);
        }

        public async Task<IEnumerable<string>> FetchEmailsAsync(string fetch)
        {
            var normalized = fetch.ToUpper();
            return await this.context.Users
                              .Where(u => u.NormalizedEmail
                                            .StartsWith(normalized, StringComparison.CurrentCultureIgnoreCase))
                              .Select(u => u.Email).ToListAsync();
        }

        public async Task<User> PromoteUserAsync(string email)
        {
            var normalized = email.ToUpper();
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.NormalizedEmail.Equals(normalized));
            if (user == null)
            {
                throw new ServiceException("Invalid email!");
            }

            if (!(await this.userManager.IsInRoleAsync(user, "User")))
            {
                throw new ServiceException("User is already an Admin!");
            }

            await this.userManager.RemoveFromRoleAsync(user, "User");
            await this.userManager.AddToRoleAsync(user, "Admin");
            return user;
        }
    }
}
