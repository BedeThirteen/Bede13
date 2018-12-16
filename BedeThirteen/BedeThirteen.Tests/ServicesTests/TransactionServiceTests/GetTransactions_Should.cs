namespace BedeThirteen.Tests.ServicesTests.TransactionServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services;
    using BedeThirteen.Services.CompositeModels;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetTransactions_Should
    {
        [TestMethod]
        public async Task ReturnTransactionsFilteredByUserIdAndAmountRange_WhenCorrectParametersArePassed()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                     .UseInMemoryDatabase($"ReturnTransactionsFilteredByUserIdAndAmountRange_WhenCorrectParametersArePassed").Options;
            User user;
            using (var context = new BedeThirteenContext(options))
            {
                var type = new TransactionType() { Name = "Deposit" };
                user = new User() { Email = "a@a.com" };
                context.TransactionTypes.Add(type);
                context.Users.Add(user);
                await context.SaveChangesAsync();

                var transactions = new List<Transaction>()
                {
                     new Transaction()
                     {
                         Date = new DateTime(2015, 1, 1),
                         Amount = 10,
                         Description = "test",
                         IsDeleted = false,
                         TransactionTypeId = type.Id,
                         UserId = user.Id
                     },
                    new Transaction()
                    {
                         Date = new DateTime(2016, 1, 1),
                         Amount = 20,
                         Description = "test2",
                         IsDeleted = false,
                         TransactionTypeId = type.Id,
                         UserId = user.Id
                     }
                };

                foreach (var tr in transactions)
                {
                    context.Transactions.Add(tr);
                }

                await context.SaveChangesAsync();
            }

            var filterBy = "amount";
            string filterCriteria = "5";
            string aditionalCriteria = "50";
            var pageSize = 2;
            var pageNumber = 0;
            var sortBy = "date_desc";
            var archiveKey = 0;
            var userId = user.Id;

            // Act
            TransactionsResult result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.GetTransactionsAsync(
                   filterBy,
                   filterCriteria,
                   aditionalCriteria,
                   pageSize,
                   pageNumber,
                   sortBy,
                   archiveKey,
                   userId);
            }

            // Assert
            Assert.AreEqual(2, result.TotalCount);
            Assert.AreEqual(2, result.Transactions.Count());
        }

        [TestMethod]
        public async Task ReturnTransactionsFilteredByDateRange_WhenCorrectParametersArePassed()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                     .UseInMemoryDatabase($"ReturnTransactionsFilteredByDateRange_WhenCorrectParametersArePassed").Options;
            User user;
            using (var context = new BedeThirteenContext(options))
            {
                var type = new TransactionType() { Name = "Deposit" };
                user = new User() { Email = "a@a.com" };
                context.TransactionTypes.Add(type);
                context.Users.Add(user);
                await context.SaveChangesAsync();

                var transactions = new List<Transaction>()
                {
                     new Transaction()
                     {
                         Date = new DateTime(2015, 1, 1),
                         Amount = 10,
                         Description = "test",
                         IsDeleted = true,
                         TransactionTypeId = type.Id,
                         UserId = user.Id
                     },
                    new Transaction()
                    {
                         Date = new DateTime(2016, 1, 1),
                         Amount = 20,
                         Description = "test2",
                         IsDeleted = true,
                         TransactionTypeId = type.Id,
                         UserId = user.Id
                     }
                };

                foreach (var tr in transactions)
                {
                    context.Transactions.Add(tr);
                }

                await context.SaveChangesAsync();
            }

            var filterBy = "date";
            string filterCriteria = new DateTime(2015, 1, 1).ToShortDateString();
            string aditionalCriteria = new DateTime(2015, 1, 2).ToShortDateString();
            var pageSize = 2;
            var pageNumber = 0;
            var sortBy = "date";
            var archiveKey = 1;
            var userId = string.Empty;

            // Act
            TransactionsResult result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                result = await sut.GetTransactionsAsync(
                   filterBy,
                   filterCriteria,
                   aditionalCriteria,
                   pageSize,
                   pageNumber,
                   sortBy,
                   archiveKey,
                   userId);
            }

            // Assert
            Assert.AreEqual(1, result.TotalCount);
            Assert.AreEqual(1, result.Transactions.Count());
        }

        //    [TestMethod]
        //    [ExpectedException(typeof(ServiceException))]
        //    public async Task ThrowServiceErrow_WhenInvalidDatesArePassed()
        //    {
        //        // Arrange
        //        var options = new DbContextOptionsBuilder<BedeThirteenContext>()
        //                 .UseInMemoryDatabase($"ThrowServiceErrow_WhenInvalidDatesArePassed")
        //                 .Options;

        //        var filterBy = "date";
        //        string filterCriteria = "invalid";
        //        string aditionalCriteria = "anotherInvalid";
        //        var pageSize = 2;
        //        var pageNumber = 0;
        //        var sortBy = "date";
        //        var archiveKey = 1;
        //        var userId = string.Empty;

        //        // Act
        //        TransactionsResult result;
        //        using (var context = new BedeThirteenContext(options))
        //        {
        //            var sut = new TransactionService(context);
        //            result = await sut.GetTransactionsAsync(
        //               filterBy,
        //               filterCriteria,
        //               aditionalCriteria,
        //               pageSize,
        //               pageNumber,
        //               sortBy,
        //               archiveKey,
        //               userId);
        //        }
        //    }
    }
}