namespace BedeThirteen.Tests.ServicesTests.TransactionServiceTests
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
    public class Stake_Should
    {
        [TestMethod]
        [DataRow("1")]
        [DataRow("2.123")]
        [DataRow("111")]
        [DataRow("0.15")]
        [DataRow("54541")]
        [DataRow("110025")]
        public async Task Stake_AnyDecimal_Amount(string amountToStake)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
              .UseInMemoryDatabase($"Stake_AnyDecimal_Amount_{amountToStake}").Options;

            decimal startingBalance = 10000000;
            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = startingBalance };

            var mockTransactionType = new TransactionType() { Name = "Stake" };

            using (var context = new BedeThirteenContext(options))
            {
                context.TransactionTypes.Add(mockTransactionType);
                context.Currencies.Add(mockCurrency);
                userToAdd.CurrencyId = mockCurrency.Id;

                context.Users.Add(userToAdd);

                context.SaveChanges();
            }

            // Act
            decimal result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.StakeAsync(userToAdd.Id, decimal.Parse(amountToStake), "Foo Game");
            }

            // Assert
            Assert.AreEqual(startingBalance - decimal.Parse(amountToStake), result);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenUserBalanceIsLessThanStaked()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                         .UseInMemoryDatabase($"ThrowException_WhenUserBalanceIsLessThanStaked").Options;

            decimal startingBalance = 50;
            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = startingBalance };

            var mockCreditCard = new CreditCard() { Number = "1234123412341234", Cvv = "123", Expiry = DateTime.Now };
            var mockTransactionType = new TransactionType() { Name = "Stake" };

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

            // Act
            decimal result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.StakeAsync(userToAdd.Id, 100, "Foo Game");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerException_When_AmountZero()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_When_AmountZero").Options;

            // Act
            decimal result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.StakeAsync("valid", 0, "Foo Game");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerException_WhenUser_DoesNotExitst()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_WhenUser_DoesNotExitst").Options;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                await sut.StakeAsync("invalidId", 100, "Foo Game");
            }
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
                var sut = new TransactionService(context);
                await sut.StakeAsync(null, 1, "Foo Game");
            }
        }

        [TestMethod]
        [DataRow("-1")]
        [DataRow("-2.14")]
        [DataRow("-111.45")]
        [DataRow("-0.4545")]
        [DataRow("-54541.4")]
        [DataRow("-11002565987.4")]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerException_When_AmountIsNegative(string amount)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_When_AmountIsNegative").Options;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);

                var result = await sut.StakeAsync("valid", decimal.Parse(amount), "Foo Game");
            }
        }
    }
}
