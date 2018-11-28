using System;
using System.Collections.Generic;
using System.Text;
using BedeThirteen.Games.GameEvaluator.Abstract;
using BedeThirteen.Games.Games.Abstract;
using BedeThirteen.Games.TokenGenerators; 

namespace BedeThirteen.Games.Games
{
   public class FourByThreeGame : AbstractGame
    {
        public FourByThreeGame( )
            : base(4, 3, new DefaultTokenGenerator(), new DefaultGameEvaluator())
        {
        }

    }
}
