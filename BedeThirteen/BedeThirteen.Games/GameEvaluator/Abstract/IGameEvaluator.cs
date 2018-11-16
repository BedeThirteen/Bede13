namespace BedeThirteen.Games.GameEvaluator.Abstract
{
    public interface IGameEvaluator
    {
        decimal CalculateCoefficient(Token[][] tokens);
    }
}