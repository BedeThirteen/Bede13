using System;

namespace BedeThirteen.Games
{
    public class Token : IEquatable<Token>
    {
        int type;
        public Token(int type = 0,decimal coefficient =0)
        {
            Type = type;
            Coefficient = coefficient;
        }

        public int Type
        {
            get => type; set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Token Type cannot be a negative number.");
                }
                type = value;
            }
        }

        public decimal Coefficient { get; set; }

        /// <summary>
        /// Comapres the tokens
        /// Type equal to 0 is considereda wildcard when comparing
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Token other)
        {
            if (other.Type == 0 || Type == 0)
            {
                return true;
            }

            return other.Type == Type;
        }
    }
}
