using BedeThirteen.App.Areas.Administration.Models;
using BedeThirteen.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BedeThirteen.App.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class HomeController : Controller
    {
        private readonly IDataAggregationService dataAggregationService;

        public HomeController(IDataAggregationService dataAggregationService)
        {
            this.dataAggregationService = dataAggregationService;
        }
        public async Task<IActionResult> Index()
        {
            var stakeSum =  await dataAggregationService.StakesSum(DateTime.MinValue, DateTime.MaxValue);
            var winSum = await dataAggregationService.WinsSum(DateTime.MinValue, DateTime.MaxValue);
            var depositSum = await dataAggregationService.DepositSum(DateTime.MinValue, DateTime.MaxValue);

            var withdrawSum = await dataAggregationService.WithdrawSum(DateTime.MinValue, DateTime.MaxValue);
 
            var model = new HomeViewModel() {
                StakeSum = decimal.Round(stakeSum, 2, MidpointRounding.AwayFromZero),
                WinSum = decimal.Round(winSum, 2, MidpointRounding.AwayFromZero) ,
                DepositeSum = decimal.Round(depositSum, 2, MidpointRounding.AwayFromZero),
                WithdrawSum = decimal.Round(withdrawSum, 2, MidpointRounding.AwayFromZero)  };
            return View(model);
        }
    }
}