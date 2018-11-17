using System;
using System.Collections.Generic;
using System.Text;

namespace BedeThirteen.Games.GameEvaluator.Abstract
{
    public class DefaultGameEvaluator : IGameEvaluator
    {
        public decimal CalculateCoefficient(Token[][] tokens)
        {
            decimal curCoef = 0;

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
                    curLineCoef += curToken.Coefficient;
                }

                if (!curLineIsDirty)
                {
                    curCoef += curLineCoef;
                }
            }
            return curCoef;
        }
    }
}
