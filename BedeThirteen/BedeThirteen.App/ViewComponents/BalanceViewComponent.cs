using BedeThirteen.App.Models.ComponentViewModels;
using BedeThirteen.Data.Models;
using BedeThirteen.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BedeThirteen.App.ViewComponents
{
    public class BalanceViewComponent : ViewComponent
    {
        private readonly IExchangeRateService exchangeRateService;
        private readonly ICurrencyService currencyService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public BalanceViewComponent(IExchangeRateService exchangeRateService, ICurrencyService currencyService,
            SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.exchangeRateService = exchangeRateService;
            this.currencyService = currencyService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (this.signInManager.IsSignedIn(HttpContext.User))
            {
                var user = await this.userManager.GetUserAsync(HttpContext.User);
                 
                if (!await userManager.IsInRoleAsync(user, "Admin"))
                {
                    var userCurrency = (await currencyService.FindCurrencyAsync(user.CurrencyId)).Name;
                    var rate = (await this.exchangeRateService.GetRatesAsync())[userCurrency];
                    var balanceVm = new BalanceViewModel()
                    {
                        Balance = Math.Round(user.Balance * rate, 2),
                        Currency = user.Currency.Name
                    };

                    return View("BalanceStatus", balanceVm);
                }
            }

            return View();
        }
    }
}