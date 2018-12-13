using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BedeThirteen.Data.Models;
using BedeThirteen.Games;
using BedeThirteen.Games.Abstract;
using BedeThirteen.Services;
using BedeThirteen.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameManager gameManager;
        private readonly ITransactionService transactionService;
        private readonly IExchangeRateService exchangeRateService;
        private readonly IUserService userService;

        public GamesController(IGameManager gameManager, ITransactionService transactionService,
            IExchangeRateService exchangeRateService, IUserService userService)
        {
            this.gameManager = gameManager;
            this.transactionService = transactionService;
            this.exchangeRateService = exchangeRateService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public IActionResult GameOne()
        {
            var game = this.gameManager.GetGame(AvailableGames.GameOne);

            return View(game.GenerateGameCombiantion());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> GameOne(decimal stakeAmount)
        {      
            string gameName = "Game One";
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.userService.GetUserAsync(userId);
            var rates = await this.exchangeRateService.GetRatesAsync();
            var userCurrency = user.Currency.Name.ToUpper();
            var userRate = rates[userCurrency];
            var stakedInUSD = stakeAmount / userRate;

            List<string> log = new List<string>();
            var currentBalance = await this.transactionService.StakeAsync(userId, stakedInUSD, gameName);

            log.Add($"Staked {stakeAmount} {userCurrency}.");

            var game = this.gameManager.GetGame(AvailableGames.GameOne);
            var currentResults = game.GenerateGameCombiantion();

            var winnings = currentResults.WinCoefficient * stakedInUSD;
            if (winnings > 0)
            {

                currentBalance = await this.transactionService.WinAsync(userId, winnings, gameName);

                log.Add($"Won {string.Concat(Math.Round(winnings* userRate, 2))} {userCurrency}.");

            }

            var balanceInUserCurrency = string.Concat(Math.Round(currentBalance * userRate, 2));


            return Json(new { newBalance = balanceInUserCurrency, currencyName = userCurrency,
                logHistory = log, rolledValues = currentResults.RolledValues,
                winningLines = currentResults.WinningLines,
                gameWinnings = string.Concat(Math.Round(winnings * userRate, 2))
            });

        }

        [HttpGet]
        [Authorize]
        public IActionResult GameTwo()
        {
            var game = this.gameManager.GetGame(AvailableGames.GameTwo);

            return View(game.GenerateGameCombiantion());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> GameTwo(decimal stakeAmount)
        {

            string gameName = "Game Two";
            var game = this.gameManager.GetGame(AvailableGames.GameTwo);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.userService.GetUserAsync(userId);
            var rates = await this.exchangeRateService.GetRatesAsync();
            var userCurrency = user.Currency.Name.ToUpper();
            var userRate = rates[userCurrency];
            var stakedInUSD = stakeAmount / userRate;

            List<string> log = new List<string>();
            var currentBalance = await this.transactionService.StakeAsync(userId, stakedInUSD, gameName);

            log.Add($"Staked {stakeAmount} {userCurrency}.");

            var currentResults = game.GenerateGameCombiantion();

            var winnings = currentResults.WinCoefficient * stakedInUSD;
            if (winnings > 0)
            {

                currentBalance = await this.transactionService.WinAsync(userId, winnings, gameName);

                log.Add($"Won {string.Concat(Math.Round(winnings * userRate, 2))} {userCurrency}.");

            }

            var balanceInUserCurrency = string.Concat(Math.Round(currentBalance * userRate, 2));


            return Json(new
            {
                newBalance = balanceInUserCurrency,
                currencyName = userCurrency,
                logHistory = log,
                rolledValues = currentResults.RolledValues,
                winningLines = currentResults.WinningLines,
                gameWinnings = string.Concat(Math.Round(winnings * userRate, 2))
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult GameThree()
        {
            var game = this.gameManager.GetGame(AvailableGames.GameThree);

            return View(game.GenerateGameCombiantion());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> GameThree(decimal stakeAmount)
        {

            string gameName = "Game Three";
            var game = this.gameManager.GetGame(AvailableGames.GameThree);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await this.userService.GetUserAsync(userId);
            var rates = await this.exchangeRateService.GetRatesAsync();
            var userCurrency = user.Currency.Name.ToUpper();
            var userRate = rates[userCurrency];
            var stakedInUSD = stakeAmount / userRate;

            List<string> log = new List<string>();
            var currentBalance = await this.transactionService.StakeAsync(userId, stakedInUSD, gameName);

            log.Add($"Staked {stakeAmount} {userCurrency}.");

            var currentResults = game.GenerateGameCombiantion();

            var winnings = currentResults.WinCoefficient * stakedInUSD;
            if (winnings > 0)
            {

                currentBalance = await this.transactionService.WinAsync(userId, winnings, gameName);

                log.Add($"Won {string.Concat(Math.Round(winnings * userRate, 2))} {userCurrency}.");

            }

            var balanceInUserCurrency = string.Concat(Math.Round(currentBalance * userRate, 2));


            return Json(new
            {
                newBalance = balanceInUserCurrency,
                currencyName = userCurrency,
                logHistory = log,
                rolledValues = currentResults.RolledValues,
                winningLines = currentResults.WinningLines,
                gameWinnings = string.Concat(Math.Round(winnings * userRate, 2))
            });
        }
    }
}