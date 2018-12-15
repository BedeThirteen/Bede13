namespace BedeThirteen.Tests.ServicesTests.CurrencyServiceTests
{
    using System;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetCurrency_Should
    {
        [TestMethod]
        public async Task ReturnCurrency_WhenIdIsValid()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ReturnCurrency_WhenIdIsValid").Options;

            var id = Guid.NewGuid();
            var currency = new Currency()
            {
                Id = id,
                Name = "test"
            };

            using (var context = new BedeThirteenContext(options))
            {
                context.Currencies.Add(currency);
                context.SaveChanges();
            }

            using (var context = new BedeThirteenContext(options))
            {
                var sut = new CurrencyService(context);
                var result = await sut.GetCurrencyAsync(id);

                Assert.IsTrue(result.Id == id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("AddCard_WhenInputs_AreValid").Options;

            using (var context = new BedeThirteenContext(options))
            {
                // Act
                var guid = Guid.NewGuid();
                var sut = new CurrencyService(context);
                await sut.GetCurrencyAsync(guid);
            }
        }
    }
}
