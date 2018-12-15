namespace BedeThirteen.Tests.ServicesTests.CurrencyServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetAllCurrencies_Should
    {
        [TestMethod]
        public async Task ReturnListOfCurrencies_WhenFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                    .UseInMemoryDatabase($"ReturnListOfCurrencies_WhenFound").Options;
            var testCurrency = new Currency() { Name = "first" };
            var secondtTestCurrency = new Currency() { Name = "second" };
            IList<Currency> results;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                context.Currencies.Add(testCurrency);
                context.Currencies.Add(secondtTestCurrency);
                context.SaveChanges();

                var sut = new CurrencyService(context);
                results = await sut.GetAllCurrenciesAsync();
            }

            using (var context = new BedeThirteenContext(options))
            {
                Assert.IsTrue(results.Count == 2);
                Assert.IsTrue(results.Any(c => c.Name == "first"));
                Assert.IsTrue(results.Any(c => c.Name == "second"));
            }
        }
    }
}
