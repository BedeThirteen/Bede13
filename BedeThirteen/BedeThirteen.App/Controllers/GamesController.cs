using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BedeThirteen.Games.Games;
using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Controllers
{
    public class GamesController : Controller
    {
        private readonly FourByThreeGame fourByThreeGame;
       

        public GamesController(FourByThreeGame fourByThreeGame  )
        {
            this.fourByThreeGame = fourByThreeGame;
          
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authenticate]
        public IActionResult GameOne()
        {
             
            return View(this.fourByThreeGame.GenerateGameCombiantion());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authenticate]
        public IActionResult GameOne(decimal stakeAmount)
        {
             
            var result = this.fourByThreeGame.GenerateGameCombiantion();

            //TODO 
            //Use Transaction service to bet stakeAmount, and if winningsCoef bigger than 0 then a winnings transacion is made            // 

            return View();

        }


    }
}