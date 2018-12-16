using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.AggregationTests
{
    [TestClass]
    public class StakeSum_Should
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(25)]
        [DataRow(123)]
        public async Task ReturnSum_WhenInput_Isvalid(int numberOfTransactions)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase($"ReturnStakeSum_WhenInput_Isvalid_{numberOfTransactions}").Options;
            var stakeTransactionType = new TransactionType() { Name = "Stake", };
            var creationTime = DateTime.Now;
            var baselineTransactionAmount = 2;
            using (var context = new BedeThirteenContext(options))
            {
                context.TransactionTypes.Add(stakeTransactionType);

                for (int i = 0; i < numberOfTransactions; i++)
                {
                    context.Transactions.Add(new Transaction()
                    {
                        CreatedOn = creationTime,
                        TransactionType = stakeTransactionType,
                        Amount = baselineTransactionAmount
                    });
                }

                context.SaveChanges();
            }

            decimal result;

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new DataAggregationService(context);
                result = await sut.StakesSum(DateTime.MinValue, DateTime.MaxValue);
            }

            // Assert
            Assert.IsTrue(result == baselineTransactionAmount * numberOfTransactions);
        }

        [TestMethod]
        public async Task ReturnStakeOfUser_WhenId_IsGiven()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("ReturnStakeSum_WhenInput_Isvalid").Options;

            var stakeTransactionType = new TransactionType() { Name = "Stake", };
            var creationTime = DateTime.Now;
            var baselineTransactionAmount = 2;
            var userAmount = 100;
            var mockUser = new User() { UserName = "Fooser" };

            using (var context = new BedeThirteenContext(options))
            {
                context.TransactionTypes.Add(stakeTransactionType);

                context.Transactions.Add(new Transaction()
                {
                    CreatedOn = creationTime,
                    TransactionType = stakeTransactionType,
                    Amount = baselineTransactionAmount
                });

                context.Transactions.Add(new Transaction()
                {
                    CreatedOn = creationTime,
                    TransactionType = stakeTransactionType,
                    Amount = userAmount,
                    User = mockUser
                });

                context.SaveChanges();
            }

            using (var context = new BedeThirteenContext(options))
            {
                var sut = new DataAggregationService(context);

                // Act
                var result = await sut.StakesSum(DateTime.MinValue, DateTime.MaxValue, mockUser.Id);

                // Assert
                Assert.IsTrue(result == userAmount);
                Assert.IsTrue(context.Transactions.Count() == 2);
                Assert.IsTrue(context.Transactions.Any(t => t.UserId != mockUser.Id));
            }
        }

        [TestMethod]
        public async Task ReturnZero_TimeInterval_IsEmpty()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("ReturnZeroStake_TimeInterval_IsEmpty").Options;

            var stakeTransactionType = new TransactionType() { Name = "Stake" };
            var creationTime = DateTime.MinValue;
            var baselineTransactionAmount = 2;

            using (var context = new BedeThirteenContext(options))
            {
                context.TransactionTypes.Add(stakeTransactionType);

                context.Transactions.Add(new Transaction()
                {
                    CreatedOn = creationTime,
                    TransactionType = stakeTransactionType,
                    Amount = baselineTransactionAmount
                });

                context.Transactions.Add(new Transaction()
                {
                    CreatedOn = creationTime,
                    TransactionType = stakeTransactionType,
                    Amount = baselineTransactionAmount
                });

                context.SaveChanges();
            }

            using (var context = new BedeThirteenContext(options))
            {
                var sut = new DataAggregationService(context);

                // Act
                var result = await sut.StakesSum(DateTime.Now, DateTime.MaxValue);

                // Assert
                Assert.IsTrue(result == 0);
            }
        }

    }
}
