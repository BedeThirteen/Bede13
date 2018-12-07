using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.UserServiceTests
{
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
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ReturnBalance_WhenUser_IsFound_{balanceToHaveStr}").Options;
            //Arrange
            var someCurrency = new Currency() { Id = new Guid(),Name="FOO"};
            var userToAdd = new User() { Balance = decimal.Parse(balanceToHaveStr)};
            

            using (var context =new BedeThirteenContext(options))
            {
                context.Currencies.Add(someCurrency);
                userToAdd.CurrencyId = someCurrency.Id;
                context.Users.Add(userToAdd);
                context.SaveChanges();
            }
            //Act
            string result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new UserService(context,null);
                result = await sut.GetUserBalanceWithCurrencyAsync(userToAdd.Id);
            }
            //Assert
            Assert.AreEqual($"{balanceToHaveStr} {someCurrency.Name}", result);

        }
    }
}
