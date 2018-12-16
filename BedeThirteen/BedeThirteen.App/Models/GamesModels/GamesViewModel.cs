using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BedeThirteen.Games;
using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Models.GamesModels
{
    public class GamesViewModel
    {
        public GamesViewModel()
        {

        }
        public GamesViewModel(string gameName, GameResult results)
        {
            GameName = gameName;
            Results = results;
        }

        public string GameName { get; set; }
        public GameResult Results { get; set; }
    }
}