using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BedeThirteen.Games;
using BedeThirteen.Games.Abstract;
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
        [Authenticate]
        public IActionResult GameOne()
        {
            var game = this.gameManager.GetGame(AvailableGames.GameOne);

            return View(game.GenerateGameCombiantion());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authenticate]
        public IActionResult GameOne(decimal stakeAmount)
        {

            var game = this.gameManager.GetGame(AvailableGames.GameOne);
            var result = game.GenerateGameCombiantion();

            //TODO 
            //Use Transaction service to bet stakeAmount, and if winningsCoef bigger than 0 then a winnings transacion is made            // 

            return View();

        }

        [HttpGet]
        [Authenticate]
        public IActionResult GameTwo()
        {
            var game = this.gameManager.GetGame(AvailableGames.GameTwo);

            return View(game.GenerateGameCombiantion());

        }

        [HttpGet]
        [Authenticate]
        public IActionResult GameThree()
        {
            var game = this.gameManager.GetGame(AvailableGames.GameThree);

            return View(game.GenerateGameCombiantion());

        }
    }
}