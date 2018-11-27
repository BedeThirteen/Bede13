using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BedeThirteen.App.Models;
using BedeThirteen.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Controllers
{
    public class UserController : Controller
    {
        private readonly ICreditCardService creditCardService;
        private readonly IBalanceService balanceService;
        private readonly IExchangeRateService exchangeRateService;
        private readonly IUserService userService;

        public UserController(ICreditCardService creditCardService,
            IBalanceService balanceService,
            IExchangeRateService exchangeRateService,
            IUserService userService)
        {
            this.creditCardService = creditCardService;
            this.balanceService = balanceService;
            this.exchangeRateService = exchangeRateService;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetUserCreditCardsAsync()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json((await this.creditCardService.GetUserCardsAsync(userId))
                                    .Select(c => new { c.Id, Number = this.Mask(c.Number) })
                                    .ToList());
        }

        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken] //TODO:  fix token
        public async Task<JsonResult> AddCreditCardAsync(CreditCardViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    throw new ArgumentException("Invalid parameters!");
            //}
            var date = new DateTime(model.Year, model.Month, 1);
            var card = await this.creditCardService.AddCreditCardAsync(
                   model.CardNumber, model.Cvv, date, this.User.FindFirstValue(ClaimTypes.NameIdentifier));


            return Json(new { card.Id, Number = this.Mask(card.Number) });
        }


        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken] //Todo: fix
        public async Task<JsonResult> DepositAmountAsync(DepositViewModel model)
        {
            //if (!ModelState.IsValid)
            //{ //Todo:
            //}
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.userService.GetUserAsync(userId);
            var rates = await this.exchangeRateService.GetRatesAsync();
            var userCurrency = user.Currency.Name.ToUpper();
            var userRate = rates[userCurrency];
            var amount = model.Amount / userRate;
            var result = await this.balanceService.Deposit(user.Id, amount, model.CardId);

            return Json(new { result = string.Concat(Math.Round(result * userRate, 2), $" {user.Currency.Name.ToUpper()}") });
        }

        private string Mask(string number)
        {
            return new string('\u2022', number.Length - 4)
                   + number.Substring(number.Length - 4);
        }
    }
}