using BedeThirteen.Games.Abstract;
using BedeThirteen.Games.Games;
using BedeThirteen.Games.Games.Abstract;
using System;

namespace BedeThirteen.Games
{
    public enum AvailableGames
    {
        GameOne, GameTwo, GameThree
    }
    public class GameManager : IGameManager
    {


        public GameManager()
        {

        }

        public IGame GetGame(AvailableGames game)
        {
            switch (game)
            {
                case AvailableGames.GameOne:
                    return new FourByThreeGame();
                    break;
                case AvailableGames.GameTwo:
                    return new FiveByFiveGame();
                    break;
                case AvailableGames.GameThree:
                    return new EighthByFiveGame();
                    break;
                default:
                    throw new ArgumentException("Oops!");    
            }
        }

    }
}
