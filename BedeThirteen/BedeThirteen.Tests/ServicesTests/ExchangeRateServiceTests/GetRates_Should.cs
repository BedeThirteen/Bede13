using BedeThirteen.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.ExchangeRateServiceTests
{
    [TestClass]
    public class GetRates_Should
    {
        [TestMethod]
        public async Task ReturnData()
        {
            //Arrange
            var sut = new ExchangeRateService();

            //Act
            var result = await sut.GetRates();

            //Assert
            Assert.IsNotNull(result);

            //act
            var result2 = await sut.GetRates();
            Assert.IsNull(result2);
        }
    }
}
