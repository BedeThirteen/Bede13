using BedeThirteen.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.ExchangeRateServiceTests
{
    [TestClass]
    public class GetRates_Should
    {
        [TestMethod]
        public async Task ReturnPropperData_WhenExternalApiIsAvailable()
        {
            //Arrange
          var sut = new ExchangeRateService();

            //Act
            var result = await sut.GetRates();

            //Assert
            Assert.AreEqual(4, result.Count);

            //act
            var secondResult = await sut.GetRates();
            Assert.AreEqual(4, secondResult.Count);
        }
    }
}
