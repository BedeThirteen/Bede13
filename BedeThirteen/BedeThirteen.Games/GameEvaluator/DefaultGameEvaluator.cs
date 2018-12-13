using System;
using System.Collections.Generic;
using System.Text;

namespace BedeThirteen.Games.GameEvaluator.Abstract
{
    public class DefaultGameEvaluator : IGameEvaluator
    {

        public (decimal ,IEnumerable<int>) CalculateCoefficientAndLines(Token[][] tokens)
        {
            decimal curCoef = 0;
            List<int> winningLines = new List<int>();

            //Checks Line by Line
            for (int y = 0; y < tokens.Length; y++)
            {
                bool curLineIsDirty = false;
                Token lastTolen = tokens[y][0];
                decimal curLineCoef = lastTolen.Coefficient;
                

                for (int x = 1; x < tokens[0].Length; x++)
                {
                    var curToken = tokens[y][x];

                    if (!lastTolen.Equals( curToken))
                    {
                        curLineIsDirty = true;
                        break;
                    }
                    //Makes sure a wildcare token does not overwrite the key token for the line
                    if (curToken.Type != 0)
                    {
                        lastTolen = curToken;

                    }
                    curLineCoef += curToken.Coefficient;
                }

                if (!curLineIsDirty)
                {
                    if (curLineCoef != 0)
                    {
                        winningLines.Add(y);

                    }
                    curCoef += curLineCoef;
                }
            }
            return (curCoef,winningLines);
        }
    }
}
