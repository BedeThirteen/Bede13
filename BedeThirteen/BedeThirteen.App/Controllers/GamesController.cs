using BedeThirteen.Games;
using BedeThirteen.Games.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameManager gameManager;

        public GamesController(IGameManager gameManager  )
        {
            this.gameManager = gameManager;
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
        public IActionResult GameOne(decimal stakeAmount)
        {

            var game = this.gameManager.GetGame(AvailableGames.GameOne);
            var result = game.GenerateGameCombiantion();

            //TODO 
            //Use Transaction service to bet stakeAmount, and if winningsCoef bigger than 0 then a winnings transacion is made            // 

            return View();

        }

        [HttpGet]
        [Authorize]
        public IActionResult GameTwo()
        {
            var game = this.gameManager.GetGame(AvailableGames.GameTwo);

            return View(game.GenerateGameCombiantion());

        }

        [HttpGet]
        [Authorize]
        public IActionResult GameThree()
        {
            var game = this.gameManager.GetGame(AvailableGames.GameThree);

            return View(game.GenerateGameCombiantion());

        }
    }
}