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
    public class Win_Should
    {
        [TestMethod]
        [DataRow("1")]
        [DataRow("2.123")]
        [DataRow("111")]
        [DataRow("0.15")]
        [DataRow("54541")]
        [DataRow("11002565987")]
        public async Task Win_AnyDecimal_Amount(string balanceToHaveStr)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                           .UseInMemoryDatabase($"Win_AnyDecimal_Amount_{balanceToHaveStr}").Options;

            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = 0 };

            var mockTransactionType = new TransactionType() { Name = "Win" };

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
                result = await sut.WinAsync(userToAdd.Id, decimal.Parse(balanceToHaveStr), "Foo Game");
            }

            // Assert
            Assert.AreEqual(balanceToHaveStr, result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_When_AmountZero()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_When_AmountZero").Options;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                var foo = await sut.WinAsync("valid", 0, "Foo Game");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_WhenUser_DoesNotExitst()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_WhenUser_DoesNotExitst").Options;

            // Act
            decimal result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.WinAsync(Guid.NewGuid().ToString(), 1, "Foo Game");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_WhenUser_IsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                           .UseInMemoryDatabase($"ThrowServerExceptio_WhenUser_IsNull")
                           .Options;

            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = 0 };

            var mockTransactionType = new TransactionType() { Name = "Deposit" };

            using (var context = new BedeThirteenContext(options))
            {
                context.TransactionTypes.Add(mockTransactionType);
                context.Currencies.Add(mockCurrency);
                userToAdd.CurrencyId = mockCurrency.Id;
                context.Users.Add(userToAdd);

                context.SaveChanges();
            }

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                await sut.WinAsync(null, 1, "Foo Game");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerException_WhenUser_IsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_WhenWinningUser_IsNull").Options;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                await sut.WinAsync(null, 1, "Foo Game");
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
        public async Task ThrowServerExceptio_When_AmountIsNegative(string balanceToHaveStr)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                           .UseInMemoryDatabase($"ThrowServerExceptio_When_AmountIsNegative")
                           .Options;

            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = 0 };

            var mockTransactionType = new TransactionType() { Name = "Deposit" };

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
                result = await sut.WinAsync(userToAdd.Id, decimal.Parse(balanceToHaveStr), "Foo Game");
            }
        }
    }
}
