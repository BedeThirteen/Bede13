using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.CurrencyServiceTests
{
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
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ReturnCurrency_WhenNameIsValid-{name}").Options;

            var currenciesToSeedWith = new[] {
                new Currency() { Name="BGN"/*,ConversionRateToUSD = 5*/},
                new Currency() { Name="USD"/*,ConversionRateToUSD = 1*/},
                new Currency() { Name="GBP"/*,ConversionRateToUSD = 2*/},
                new Currency() { Name="YEN"/*,ConversionRateToUSD = 0.01m*/}


            };

            using (var context = new BedeThirteenContext(options))
            {
               
                foreach (var currency in currenciesToSeedWith)
                {
                    context.Currencies.Add(currency);
                }
                context.SaveChanges();


            }

            using (var context = new BedeThirteenContext(options))
            {
                var sut = new CurrencyService(context);
                var result = await sut.GetCurrencyByNameAsync(name);

                Assert.IsTrue(result.Name == name);
               // Assert.IsTrue(result.ConversionRateToUSD == decimal.Parse(conversionRate));
            }
        }
    }
}
