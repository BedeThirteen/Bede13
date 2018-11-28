using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using BedeThirteen.Games.TokenGenerators.Abstract;

namespace BedeThirteen.Games.TokenGenerators
{
    public class DefaultTokenGenerator : ITokenGenerator
    {
         

        public DefaultTokenGenerator()
        {
        }

        public Token GenerateToken()
        {
            RNGCryptoServiceProvider  RNGProvider = new RNGCryptoServiceProvider();

            var byteArray = new byte[4];
            RNGProvider.GetBytes(byteArray);

            //convert 4 bytes to an integer value between 0 and 100
            var randomInteger = BitConverter.ToUInt32(byteArray, 0) % 100;
             

            if (randomInteger < 45)
            {
                return new Token(1, 0.4m);
            }
            else if(randomInteger < 45+35)
            {
                return new Token(2,0.6m);

            }
            else if (randomInteger < 45 + 35 +15)
            {
                return new Token(3,0.8m);

            }
            else
            {
                return new Token(0);
            }
        }
    }
}
