using BedeThirteen.Games;
using BedeThirteen.Games.GameEvaluator.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BedeThirteen.Tests.GamesTests.GameEvaluaterTests
{
    [TestClass]
    public class DefaultGameEvaluater_Should
    {
        [DataTestMethod]
        [DataRow("1")]
        [DataRow("2")]
        [DataRow("100")]
        [DataRow("0.0")]
        [DataRow("0.33")]
        [DataRow("0.314")]
        [DataRow("0.778")]
        [DataRow("13.37")]
        public void Returns_Corect_Coef(string coefAsStr)
        {
            var testingToken = new Token(1, decimal.Parse(coefAsStr));
            Token[][] tokens = new Token[][] { new Token[] { testingToken, testingToken, testingToken } };

            var sut = new DefaultGameEvaluator();

            var res = sut.CalculateCoefficient(tokens);

            Assert.IsTrue(res == testingToken.Coefficient * 3);
        }

        [TestMethod]
        public void Work_Corectly_WithLeftMostWildcard()
        {
            var testingToken = new Token(1, 5);
            var wildcard = new Token();
            Token[][] tokens = new Token[][] { new Token[] { wildcard, testingToken, testingToken } };

            var sut = new DefaultGameEvaluator();

            var res = sut.CalculateCoefficient(tokens);

            Assert.IsTrue(res == testingToken.Coefficient * 2);
        }

        [TestMethod]
        public void Work_Corectly_WithMiddleWildcard()
        {
            var testingToken = new Token(1, 5);
            var wildcard = new Token();
            Token[][] tokens = new Token[][] { new Token[] { testingToken,  wildcard, testingToken } };

            var sut = new DefaultGameEvaluator();

            var res = sut.CalculateCoefficient(tokens);

            Assert.IsTrue(res == testingToken.Coefficient * 2);
        }

        [TestMethod]
        public void Work_Corectly_WithMostRightWildcard()
        {
            var testingToken = new Token(1, 5);
            var wildcard = new Token();
            Token[][] tokens = new Token[][] { new Token[] { testingToken, testingToken, wildcard } };

            var sut = new DefaultGameEvaluator();

            var res = sut.CalculateCoefficient(tokens);

            Assert.IsTrue(res == testingToken.Coefficient * 2);
        }

        [TestMethod]
        public void ReturnZeroCoef_WhenTokens_DontMatch()
        {
            Token[][] tokens = new Token[][] { new Token[] { new Token(1, 5), new Token(2, 12), new Token(3, 7) } };

            var sut = new DefaultGameEvaluator();

            var res = sut.CalculateCoefficient(tokens);

            Assert.IsTrue(res == 0);
        }
        [DataTestMethod]
        [DataRow("1", 55)]
        [DataRow("2", 8)]
        [DataRow("100", 3)]
        [DataRow("0.0", 15)]
        [DataRow("0.33", 9)]
        [DataRow("0.314", 6)]
        [DataRow("0.778", 5)]
        [DataRow("13.37", 3)]
        public void ReturnsCorectCoef_WhenThreare_MultipleRows(string coefAsStr, int numberOfRows)
        {
            var testingToken = new Token(1, decimal.Parse(coefAsStr));
            var baseRow = new Token[] { testingToken, testingToken, testingToken };
            Token[][] tokens = new Token[numberOfRows][];
            for (int i = 0; i < numberOfRows; i++)
            {
                tokens[i] = baseRow;
            }

            var sut = new DefaultGameEvaluator();

            var res = sut.CalculateCoefficient(tokens);

            Assert.IsTrue(res == testingToken.Coefficient * 3 * numberOfRows);
        }
    }
}