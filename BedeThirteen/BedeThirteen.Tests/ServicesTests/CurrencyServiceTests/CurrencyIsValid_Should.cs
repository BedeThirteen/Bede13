namespace BedeThirteen.Tests.ServicesTests.CurrencyServiceTests
{
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CurrencyIsValid_Should
    {
        [TestMethod]
        [DataRow("Test")]
        [DataRow("BGN")]
        [DataRow("RuB")]
        [DataRow("USD")]
        [DataRow("usd")]
        public async Task ReturnTrue_WhenCurrencyName_Isvalid(string otherCurrencyName)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                    .UseInMemoryDatabase($"ReturnTrue_WhenCurrencyName_Isvalid-{otherCurrencyName}").Options;

            using (var context = new BedeThirteenContext(options))
            {
                var testCurrency = new Currency() { Name = otherCurrencyName };
                context.Currencies.Add(testCurrency);
                context.SaveChanges();
            }

            // Act && Assert
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new CurrencyService(context);
                var result = await sut.CurrencyIsValidAsync(otherCurrencyName);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("BGN")]
        [DataRow("RuB")]
        [DataRow("USD")]
        [DataRow("usd")]
        public async Task ReturnFalse_WhenCurrencyName_IsInvalid(string otherCurrencyName)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                    .UseInMemoryDatabase($"ReturnFalse_WhenCurrencyName_IsInvalid-{otherCurrencyName}").Options;

            using (var context = new BedeThirteenContext(options))
            {
                var testCurrency = new Currency() { Name = "lev" };
                context.Currencies.Add(testCurrency);
                context.SaveChanges();
            }

            // Act && Assert
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new CurrencyService(context);
                var result = await sut.CurrencyIsValidAsync(otherCurrencyName);

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        [DataRow("yen", "YEN")]
        [DataRow("BgN", "BGN")]
        [DataRow("rUb", "RUB")]
        [DataRow("USD", "USD")]
        public async Task Ingnore_CaseSensitivity(string currencyName, string exprected)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                    .UseInMemoryDatabase($"Ingnore_CaseSensitivity-{currencyName}").Options;

            using (var context = new BedeThirteenContext(options))
            {
                var testCurrency = new Currency() { Name = exprected };
                context.Currencies.Add(testCurrency);
                context.SaveChanges();
            }

            using (var context = new BedeThirteenContext(options))
            {
                var sut = new CurrencyService(context);
                var result = await sut.CurrencyIsValidAsync(currencyName);

                Assert.IsTrue(result);
            }
        }
    }
}
