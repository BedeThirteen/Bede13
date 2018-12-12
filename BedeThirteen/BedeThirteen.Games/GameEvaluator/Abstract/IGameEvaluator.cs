using System.Collections.Generic;

namespace BedeThirteen.Games.GameEvaluator.Abstract
{
    public interface IGameEvaluator
    {
        (decimal, IEnumerable<int>) CalculateCoefficientAndLines(Token[][] tokens);
    }
}