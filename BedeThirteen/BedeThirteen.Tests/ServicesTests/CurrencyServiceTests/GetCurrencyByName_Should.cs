namespace BedeThirteen.Tests.ServicesTests.CurrencyServiceTests
{
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetCurrencyByName_Should
    {
        [TestMethod]
        [DataRow("GBP", "2")]
        [DataRow("USD", "1")]
        [DataRow("BGN", "5")]
        [DataRow("YEN", "0.01")]
        public async Task ReturnCurrency_WhenNameIsValid(string name, string conversionRate)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ReturnCurrency_WhenNameIsValid-{name}").Options;

            var currenciesToSeedWith = new[] {
                new Currency() { Name = "BGN" },
                new Currency() { Name = "USD" },
                new Currency() { Name = "GBP" },
                new Currency() { Name = "YEN" }
            };

            using (var context = new BedeThirteenContext(options))
            {
                foreach (var currency in currenciesToSeedWith)
                {
                    context.Currencies.Add(currency);
                }

                context.SaveChanges();
            }

            // Act && Assert
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new CurrencyService(context);
                var result = await sut.GetCurrencyByNameAsync(name);

                Assert.IsTrue(result.Name == name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("AddCard_WhenInputs_AreValid").Options;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new CurrencyService(context);
                await sut.GetCurrencyByNameAsync("invalid");
            }
        }
    }
}
