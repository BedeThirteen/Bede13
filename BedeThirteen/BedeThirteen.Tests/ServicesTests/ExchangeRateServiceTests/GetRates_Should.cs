namespace BedeThirteen.Tests.ServicesTests.ExchangeRateServiceTests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BedeThirteen.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetRates_Should
    {
        [TestMethod]
        public async Task ReturnDictionaryWithRates()
        {
            // Arrange
            var sut = new ExchangeRateService();

            // Act
            var result = await sut.GetRatesAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Dictionary<string, decimal>));
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        [DataRow("USD")]
        [DataRow("BGN")]
        [DataRow("EUR")]
        [DataRow("GBP")]
        public async Task ReturnDictionaryWithRatesAsKeys_USE_EUR_BGN_GBP(string currency)
        {
            // Arrange
            var sut = new ExchangeRateService();

            // Act
            var result = await sut.GetRatesAsync();

            // Assert
            Assert.IsTrue(result.ContainsKey(currency));
        }

        [TestMethod]
        [DataRow("USD")]
        [DataRow("BGN")]
        [DataRow("EUR")]
        [DataRow("GBP")]
        public async Task ReturnDictionary_WithValidValues(string currency)
        {
            // Arrange
            var sut = new ExchangeRateService();

            // Act
            var result = await sut.GetRatesAsync();

            // Assert
            Assert.IsInstanceOfType(result[currency], typeof(decimal));
            Assert.IsTrue(result[currency] > 0);
        }
    }
}
