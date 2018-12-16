using BedeThirteen.Games;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BedeThirteen.Tests.GamesTests.TokenTests
{
    [TestClass]
    public class Equals_Should
    {
        [TestMethod]
        public void ReturnFalse_WhenOtherToken_DoesntMatch()
        {
            // Arrange
            var token1 = new Token(1);
            var token2 = new Token(2);
            // Act
            var result = token1.Equals(token2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(1, 5)]
        [DataRow(4, 5)]
        [DataRow(0, 0)]
        [DataRow(7, 7)]
        [DataRow(12, 75)]
        [DataRow(10000, 1)]
        public void NotDepend_OnThe_Caller(int type1, int type2)
        {
            // Arrange
            var token1 = new Token(type1);
            var token2 = new Token(type2);

            // Act
            var result1 = token1.Equals(token2);
            var result2 = token2.Equals(token1);

            // Assert
            Assert.AreEqual(result1, result2);
        }
    }
}
