using System;
using System.Collections.Generic;
using System.Text;
using BedeThirteen.Games.GameEvaluator.Abstract;
using BedeThirteen.Games.Games.Abstract;
using BedeThirteen.Games.TokenGenerators; 

namespace BedeThirteen.Games.Games
{
   public class FiveByFiveGame : AbstractGame
    {
        public FiveByFiveGame( )
            : base(5, 5, new DefaultTokenGenerator(), new DefaultGameEvaluator())
        {
        }
    }
}
