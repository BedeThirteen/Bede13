using BedeThirteen.Games.Games.Abstract;

namespace BedeThirteen.Games.Abstract
{
    public interface IGameManager
    {
        IGame GetGame(AvailableGames game);
    }
}