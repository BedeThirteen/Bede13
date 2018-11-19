using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.CurrencyServiceTests
{
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
            //Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                    .UseInMemoryDatabase($"ReturnTrue_WhenCurrencyName_Isvalid").Options;

            using (var context = new BedeThirteenContext(options))
            {
                var testCurrency = new Currency() { Name = otherCurrencyName };
                context.Currencies.Add(testCurrency);
                context.SaveChanges();
            }


            using (var context = new BedeThirteenContext(options))
            {
                var sut = new CurrencyService(context);
                var result = await sut.CurrencyIsValidAsync(otherCurrencyName);

                Assert.IsTrue(result);
            }
        }
    }
}
