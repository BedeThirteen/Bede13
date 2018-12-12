using BedeThirteen.Games.GameEvaluator.Abstract;
using BedeThirteen.Games.TokenGenerators.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BedeThirteen.Games.Games.Abstract
{
    public class AbstractGame : IGame
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IGameEvaluator _gameEvaluator;

        public AbstractGame(int rows, int columns, ITokenGenerator tokenGenerator, IGameEvaluator gameEvaluator)
        {
            Rows = rows;
            Columns = columns;
            _tokenGenerator = tokenGenerator;
            _gameEvaluator = gameEvaluator;
        }

        public int Rows { get; protected set; }
        public int Columns { get; protected set; }

        public GameResult GenerateGameCombiantion()
        {
            var slots = new Token[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                slots[i] = new Token[Columns];
            }

            //Tokens are generated column by column
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    slots[y][x] = _tokenGenerator.GenerateToken();
                }
            }
            (decimal winCoefficient, IEnumerable<int> winningLines) = _gameEvaluator.CalculateCoefficientAndLines(slots);

            var curGameResult = new GameResult(){ RolledValues = slots ,WinCoefficient = winCoefficient,WinningLines =winningLines};
            return curGameResult;
        }
    }
}
