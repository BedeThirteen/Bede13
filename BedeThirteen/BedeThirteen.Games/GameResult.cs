using System;
using System.Collections.Generic;

namespace BedeThirteen.Games
{
    public class GameResult
    {
        public decimal WinCoefficient { get; set; }
        public Token[][] RolledValues { get; set; }
        public IEnumerable<int> WinningLines { get; set; }

    }
}
