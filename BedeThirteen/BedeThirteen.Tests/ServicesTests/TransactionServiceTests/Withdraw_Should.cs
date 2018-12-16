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
    public class Withdraw_Should
    {
        [TestMethod]
        [DataRow("1")]
        [DataRow("2.123")]
        [DataRow("111")]
        [DataRow("0.15")]
        [DataRow("54541")]
        [DataRow("110025")]
        public async Task Withdraw_AnyDecimal_Amount(string amountToWithdraw)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                          .UseInMemoryDatabase($"Withdraw_AnyDecimal_Amount_{amountToWithdraw}")
                          .Options;

            decimal startingBalance = 10000000;
            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = startingBalance };

            var mockCreditCard = new CreditCard() { Number = "1234123412341234", Cvv = "123", Expiry = DateTime.Now };
            var mockTransactionType = new TransactionType() { Name = "Withdraw" };

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
                result = await sut.WithdrawAsync(userToAdd.Id, decimal.Parse(amountToWithdraw), mockCreditCard.Id);
            }

            // Assert
            Assert.AreEqual(startingBalance - decimal.Parse(amountToWithdraw), result);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_When_AmountZero()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                        .UseInMemoryDatabase($"ThrowServerExceptio_When_AmountZero").Options;

            var startingBalance = 10000000;
            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = startingBalance };

            var mockCreditCard = new CreditCard() { Number = "1234123412341234", Cvv = "123", Expiry = DateTime.Now };
            var mockTransactionType = new TransactionType() { Name = "Withdraw" };

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
                result = await sut.WithdrawAsync(userToAdd.Id, 0, mockCreditCard.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_WhenUser_DoesNotExitst()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                          .UseInMemoryDatabase($"ThrowServerExceptio_WhenUser_DoesNotExitst").Options;

            decimal startingBalance = 10000000;

            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = startingBalance };

            var mockCreditCard = new CreditCard() { Number = "1234123412341234", Cvv = "123", Expiry = DateTime.Now };
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

            // Act
            decimal result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.WithdrawAsync(Guid.NewGuid().ToString(), 1, mockCreditCard.Id);
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

            decimal startingBalance = 10000000;
            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = startingBalance };

            var mockCreditCard = new CreditCard() { Number = "1234123412341234", Cvv = "123", Expiry = DateTime.Now };
            var mockTransactionType = new TransactionType() { Name = "Withdraw" };

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
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                await sut.DepositAsync(null, 1, mockCreditCard.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_WhenCard_IsNotRegistered_ToUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                          .UseInMemoryDatabase($"ThrowServerExceptio_WhenCard_IsNotRegistered_ToUser")
                          .Options;

            decimal startingBalance = 10000000;
            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = startingBalance };

            var mockCreditCard = new CreditCard() { Number = "1234123412341234", Cvv = "123", Expiry = DateTime.Now };
            var mockTransactionType = new TransactionType() { Name = "Withdraw" };

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

            var cardIdNotRegisteredToUser = Guid.NewGuid();

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                await sut.WithdrawAsync(userToAdd.Id, 1m, cardIdNotRegisteredToUser);
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

            decimal startingBalance = 10000000;
            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = startingBalance };

            var mockCreditCard = new CreditCard() { Number = "1234123412341234", Cvv = "123", Expiry = DateTime.Now };
            var mockTransactionType = new TransactionType() { Name = "Withdraw" };

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
                result = await sut.WithdrawAsync(userToAdd.Id, decimal.Parse(balanceToHaveStr), mockCreditCard.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowServerExceptio_WhenBalanceIsLessThanAmount()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                          .UseInMemoryDatabase($"ThrowServerExceptio_WhenBalanceIsLessThanAmount")
                          .Options;

            decimal startingBalance = 50;
            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            var userToAdd = new User() { Balance = startingBalance };

            var mockCreditCard = new CreditCard() { Number = "1234123412341234", Cvv = "123", Expiry = DateTime.Now };
            var mockTransactionType = new TransactionType() { Name = "Withdraw" };

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
                result = await sut.WithdrawAsync(userToAdd.Id, 100, mockCreditCard.Id);
            }
        }
    }
}
