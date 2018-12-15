namespace BedeThirteen.Tests.ServicesTests.CreditCardServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AddCreditCard_Should
    {
        [TestMethod]
        public async Task AddCard_WhenInputs_AreValid()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("AddCard_WhenInputs_AreValid").Options;

            var cardToAdd = new CreditCard()
            {
                Number = "1234567889101112",
                Cvv = "123",
                UserId = "totalyaGuid"
            };
            var userToAdd = new User() { Id = "totalyaGuid" };

            using (var context = new BedeThirteenContext(options))
            {
                context.Users.Add(userToAdd);
                await context.SaveChangesAsync();
                // Act
                var sut = new CreditCardService(context);
                await sut.AddCreditCardAsync(cardToAdd.Number, cardToAdd.Cvv, DateTime.Now, userToAdd.Id);
            }

            using (var context = new BedeThirteenContext(options))
            {
                Assert.IsTrue(context.CreditCards.Any());
                Assert.IsTrue(context.CreditCards.Any(cc => cc.Number == cardToAdd.Number));
                Assert.IsTrue(context.CreditCards.Any(cc => cc.Cvv == cardToAdd.Cvv));
                Assert.IsTrue(context.CreditCards.Any(cc => cc.UserId == userToAdd.Id));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenCardValues_AreNull()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("ThrowException_WhenCardValues_AreNull").Options;

            var cardToAdd = new CreditCard()
            {
            };
            var userToAdd = new User();

            using (var context = new BedeThirteenContext(options))
            {
                context.Users.Add(userToAdd);

                // Act
                var sut = new CreditCardService(context);
                await sut.AddCreditCardAsync(cardToAdd.Number, cardToAdd.Cvv, DateTime.Now, userToAdd.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenUser_DoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("ThrowException_WhenUser_DoesNotExist").Options;

            var cardToAdd = new CreditCard()
            {
                Number = "1234567889101112",
                Cvv = "123",
                UserId = "totalyaGuid"
            };

            using (var context = new BedeThirteenContext(options))
            {
                // Act
                var sut = new CreditCardService(context);
                await sut.AddCreditCardAsync(cardToAdd.Number, cardToAdd.Cvv, DateTime.Now, cardToAdd.UserId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenUser_UserAlreadyHasCardWithThatNumber()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("ThrowException_WhenUser_UserAlreadyHasCardWithThatNumber")
                .Options;

            var card = new CreditCard()
            {
                Number = "1234567889101112",
                Cvv = "123",
                UserId = "totalyaGuid"
            };

            var userToAdd = new User()
            {
                Id = "totalyaGuid",
            };

            using (var context = new BedeThirteenContext(options))
            {
                context.Users.Add(userToAdd);
                await context.SaveChangesAsync();

                // Act
                var sut = new CreditCardService(context);

                await sut.AddCreditCardAsync(
                    card.Number,
                    card.Cvv,
                    DateTime.Now,
                    userToAdd.Id);

                await sut.AddCreditCardAsync(
                  card.Number,
                  card.Cvv,
                  DateTime.Now,
                  userToAdd.Id);
            }
        }
    }
}
