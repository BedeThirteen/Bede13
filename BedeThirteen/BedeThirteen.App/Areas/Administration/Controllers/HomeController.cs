using BedeThirteen.App.Areas.Administration.Models;
using BedeThirteen.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BedeThirteen.App.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class HomeController : Controller
    {
        private readonly IDataAggregationService dataAggregationService;
        private readonly IUserService userService;


        public HomeController(IDataAggregationService dataAggregationService, IUserService userService)
        {
            this.dataAggregationService = dataAggregationService;
            this.userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var stakeSum = await dataAggregationService.StakesSum(DateTime.MinValue, DateTime.MaxValue);
            var winSum = await dataAggregationService.WinsSum(DateTime.MinValue, DateTime.MaxValue);
            var depositSum = await dataAggregationService.DepositSum(DateTime.MinValue, DateTime.MaxValue);

            var withdrawSum = await dataAggregationService.WithdrawSum(DateTime.MinValue, DateTime.MaxValue);

            var model = new HomeViewModel()
            {
                StakeSum = decimal.Round(stakeSum, 2, MidpointRounding.AwayFromZero),
                WinSum = decimal.Round(winSum, 2, MidpointRounding.AwayFromZero),
                DepositeSum = decimal.Round(depositSum, 2, MidpointRounding.AwayFromZero),
                WithdrawSum = decimal.Round(withdrawSum, 2, MidpointRounding.AwayFromZero)
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Promote(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Invalid parameter!");
            }
            if (await this.userService.PromoteUserAsync(email) == null)
            {
                throw new ArgumentException("Invalid parameter!");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<JsonResult> AutoCompleteAsync(string term)
        {
            return base.Json((await this.userService.FetchEmailsAsync(term))
                                        .Select(e => new { Email = e }).ToList());
        }
    }
}