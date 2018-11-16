﻿using System;
using System.Collections.Generic;
using System.Text;
using BedeThirteen.Games.GameEvaluator.Abstract;
using BedeThirteen.Games.Games.Abstract;
using BedeThirteen.Games.TokenGenerators; 

namespace BedeThirteen.Games.Games
{
   public class EighthByFiveGame : AbstractGame
    {
        public EighthByFiveGame( )
            : base(8, 5, new DefaultTokenGenerator(), new DefaultGameEvaluator())
        {
        }
    }
}
