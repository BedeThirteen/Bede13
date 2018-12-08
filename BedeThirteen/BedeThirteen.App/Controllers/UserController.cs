﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BedeThirteen.App.Models;
using BedeThirteen.Services;
using BedeThirteen.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Controllers
{
    public class UserController : Controller
    {
        private readonly ICreditCardService creditCardService;
        private readonly IUserService userService;
        private readonly IExchangeRateService exchangeRateService;
        private readonly ITransactionService transactionService;

        public UserController(ICreditCardService creditCardService,
            IExchangeRateService exchangeRateService,
            IUserService userService,
            ITransactionService transactionService)
        {
            this.creditCardService = creditCardService;
            this.exchangeRateService = exchangeRateService;
            this.userService = userService;
            this.transactionService = transactionService;
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetUserCreditCardsAsync()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cards = await this.creditCardService.GetUserCardsAsync(userId);
            var model = cards.Select(c => new { c.Id, Number = this.Mask(c.Number) })
                                    .ToList();

            return Json((await this.creditCardService.GetUserCardsAsync(userId))
                                    .Select(c => new { c.Id, Number = this.Mask(c.Number) })
                                    .ToList());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddCreditCardAsync(CreditCardViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    throw new ArgumentException("Invalid parameters!");
            //}

            if (model.Month < DateTime.Now.Month && model.Year < DateTime.Now.Year)
            {
                throw new ArgumentException("Invalid expiration date!");
            }

            var date = new DateTime(model.Year, model.Month, 1);
            var card = await this.creditCardService.AddCreditCardAsync(
                   model.CardNumber, model.Cvv, date, this.User.FindFirstValue(ClaimTypes.NameIdentifier));


            return Json(new { card.Id, Number = this.Mask(card.Number) });
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> DepositAmountAsync(MoneyAmountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                  return View(ModelState);
            }

           var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.userService.GetUserAsync(userId);
            var rates = await this.exchangeRateService.GetRatesAsync();
            var userCurrency = user.Currency.Name.ToUpper();
            var userRate = rates[userCurrency];
            var amount = model.Amount / userRate;
            var result = await this.transactionService.DepositAsync(user.Id, amount, model.CardId);
            return Json(new { result = string.Concat(Math.Round(result * userRate, 2), $" {user.Currency.Name.ToUpper()}") });
             
            
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> WithdrawAmountAsync(MoneyAmountViewModel model)
        {
            //if (!ModelState.IsValid)
            //{ //Todo:
            //}
            var user = await this.userService.GetUserAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var rates = await this.exchangeRateService.GetRatesAsync();
            var userRate = rates[user.Currency.Name.ToUpper()];
            var result = await this.transactionService.WithdrawAsync(user.Id, model.Amount / userRate, model.CardId);

            return Json(new { result = string.Concat(Math.Round(result * userRate, 2), $" {user.Currency.Name.ToUpper()}") });
        }

        private string Mask(string number)
        {
            return new string('\u2022', number.Length - 4)
                   + number.Substring(number.Length - 4);
        }
    }
}