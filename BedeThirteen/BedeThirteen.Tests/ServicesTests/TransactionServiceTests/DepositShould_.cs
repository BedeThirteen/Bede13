using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.TransactionServiceTests
{
    [TestClass]
    public class DepositShould_
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
        [DataRow("-0")]
        [DataRow("-54541")]
        [DataRow("-11002565987")]
        public async Task Deposit_AnyDecimal_Amount(string balanceToHaveStr)
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"Deposit_AnyDecimal_Amount_{balanceToHaveStr}").Options;

            //Arrange
            var mockCurrency = new Currency() { Id = new Guid(), Name = "FOO" };
            var userToAdd = new User() { Balance = 0 };

            var mockCreditCard = new CreditCard() {  Number = "1234123412341234",Cvv="123",Expiry = DateTime.Now };
            var mockTransactionType = new TransactionType() { Name = "Deposit" };

            using (var context = new BedeThirteenContext(options))
            {
                context.TransactionTypes.Add(mockTransactionType);
                context.Currencies.Add(mockCurrency);
                userToAdd.CurrencyId = mockCurrency.Id;
                context.Users.Add(userToAdd);
                mockCreditCard.User = userToAdd;
                context.CreditCards.Add(mockCreditCard);
                context.SaveChanges();
            }
            //Act
            decimal result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.DepositAsync(userToAdd.Id , decimal.Parse( balanceToHaveStr),mockCreditCard.Id);
            }
            //Assert
            Assert.AreEqual(balanceToHaveStr, result.ToString());

        }
    }
}
