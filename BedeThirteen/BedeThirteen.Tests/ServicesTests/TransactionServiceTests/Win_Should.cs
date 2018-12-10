using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using BedeThirteen.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.TransactionServiceTests
{
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
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"Win_AnyDecimal_Amount_{balanceToHaveStr}").Options;

            //Arrange
            var mockCurrency = new Currency() { Id = new Guid(), Name = "FOO" };
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
            //Act
            decimal result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.WinAsync(userToAdd.Id, decimal.Parse(balanceToHaveStr), "Foo Game");

            }
            //Assert
            Assert.AreEqual(balanceToHaveStr, result.ToString());

        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_When_AmountZero()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_When_AmountZero").Options;

            //Arrange
            var mockCurrency = new Currency() { Id = new Guid(), Name = "FOO" };
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
            //Act

            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                var foo = sut.WinAsync(userToAdd.Id, 0, "Foo Game");

            }


        }


        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_WhenUser_DoesNotExitst()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_WhenUser_DoesNotExitst").Options;

            //Arrange
            var mockCurrency = new Currency() { Id = new Guid(), Name = "FOO" };
            var userToAdd = new User() { Balance = 0 };

            var mockCreditCard = new CreditCard() { Number = "1234123412341234", Cvv = "123", Expiry = DateTime.Now };
            var mockTransactionType = new TransactionType() { Name = "Win" };

            using (var context = new BedeThirteenContext(options))
            {
                context.TransactionTypes.Add(mockTransactionType);
                context.Currencies.Add(mockCurrency);
                userToAdd.CurrencyId = mockCurrency.Id;
                context.Users.Add(userToAdd);
                context.SaveChanges();
            }

            //Act
            decimal result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.DepositAsync((new Guid()).ToString(), 1, mockCreditCard.Id);
                result = await sut.WinAsync((new Guid()).ToString(), 1, "Foo Game");
            }


        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_WhenUser_IsNull()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_WhenUser_IsNull").Options;

            //Arrange
            var mockCurrency = new Currency() { Id = new Guid(), Name = "FOO" };
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
            //Act

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
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ThrowServerExceptio_When_AmountIsNegative").Options;

            //Arrange
            var mockCurrency = new Currency() { Id = new Guid(), Name = "FOO" };
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
            //Act
            decimal result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.WinAsync(userToAdd.Id, decimal.Parse(balanceToHaveStr), "Foo Game");

            }

        }
    }
}
