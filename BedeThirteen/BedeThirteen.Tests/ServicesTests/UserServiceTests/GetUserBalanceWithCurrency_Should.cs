namespace BedeThirteen.Tests.ServicesTests.UserServiceTests
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
    public class GetUserBalanceWithCurrency_Should
    {
        [TestMethod]
        [DataRow("1")]
        [DataRow("2")]
        [DataRow("111")]
        [DataRow("0")]
        [DataRow("54541")]
        [DataRow("11002565987")]
        [DataRow("-1")]
        [DataRow("-2")]
        [DataRow("-111")]
        [DataRow("2312321213210")]
        [DataRow("-54541")]
        [DataRow("-11002565987")]
        public async Task ReturnBalance_WhenUser_IsFound(string balanceToHaveStr)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                           .UseInMemoryDatabase($"ReturnBalance_WhenUser_IsFound_{balanceToHaveStr}")
                           .Options;

            var someCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = decimal.Parse(balanceToHaveStr) };
            using (var context = new BedeThirteenContext(options))
            {
                context.Currencies.Add(someCurrency);
                userToAdd.CurrencyId = someCurrency.Id;
                context.Users.Add(userToAdd);
                context.SaveChanges();
            }

            // Act
            string result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new UserService(context, null);
                result = await sut.GetUserBalanceWithCurrencyAsync(userToAdd.Id);
            }

            // Assert
            Assert.AreEqual($"{balanceToHaveStr} {someCurrency.Name}", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerException_WhenUser_IsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_WhenUser_IsNull").Options;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new UserService(context, null);
                await sut.GetUserBalanceWithCurrencyAsync(null);
            }
        }
    }
}