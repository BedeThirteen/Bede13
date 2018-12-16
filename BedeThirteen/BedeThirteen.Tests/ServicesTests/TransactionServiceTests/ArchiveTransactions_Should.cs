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
    public class ArchiveTransactions_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenNoTransactionsAreFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
              .UseInMemoryDatabase($"ThrowException_WhenNoTransactionsAreFound").Options;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                var result = await sut.ArchiveTransactionsAsync(
                    new DateTime(2000, 1, 1), new DateTime(2000, 1, 2));
            }
        }

        [TestMethod]
        public async Task ReturnCountOfArchivedTransactions_WhenCriteriaIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
              .UseInMemoryDatabase($"ReturnCountOfArchivedTransactions_WhenCriteriaIsValid")
              .Options;

            var transactionsToSeedWith = new[]
            {
                new Transaction() { Date = new DateTime(2000, 1, 1), IsDeleted = false },
                new Transaction() { Date = new DateTime(2005, 1, 1), IsDeleted = false },
            };

            using (var context = new BedeThirteenContext(options))
            {
                foreach (var transaction in transactionsToSeedWith)
                {
                    context.Transactions.Add(transaction);
                }

                context.SaveChanges();
            }

            int actual;
            int expected = 1;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new TransactionService(context);
                actual = await sut.ArchiveTransactionsAsync(
                   new DateTime(2000, 1, 1), new DateTime(2001, 1, 2));
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
