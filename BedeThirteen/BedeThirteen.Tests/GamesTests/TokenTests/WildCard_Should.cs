namespace BedeThirteen.Tests.GamesTests.TokenTests
{
    using BedeThirteen.Games;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WildCard_Should
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(100)]
        [DataRow(0)]
        public void Equal_AnyOtherToken(int otherTokenType)
        {
            // Arrange
            var otherToken = new Token(otherTokenType);
            var sut = new Token(0);

            // Act
            var res = sut.Equals(otherToken);
            Assert.IsTrue(res);
        }
    }
}
