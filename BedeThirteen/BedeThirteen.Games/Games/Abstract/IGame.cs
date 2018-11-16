namespace BedeThirteen.Games.Games.Abstract
{
    public interface IGame
    {
        int Columns { get; }
        int Rows { get; }

        GameResult GenerateGameCombiantion();
    }
}