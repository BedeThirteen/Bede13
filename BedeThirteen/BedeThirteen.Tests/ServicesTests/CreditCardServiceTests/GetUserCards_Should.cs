namespace BedeThirteen.Tests.ServicesTests.CreditCardServiceTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetUserCards_Should
    {
        [TestMethod]
        public async Task GetCards_WhenInputs_AreValid()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("GetCards_WhenInputs_AreValid").Options;

            var cardToSeedWith = new[]
            {
                   new CreditCard()
                  {
                      Number = "1234567889101112",
                      Cvv = "123"
                  },
                   new CreditCard()
                  {
                      Number = "121110987654321",
                      Cvv = "321"
                  },
                   new CreditCard()
                  {
                      Number = "111110987654321",
                      Cvv = "321"
                  }
            };

            var cardFromDiffrentUser = new CreditCard()
            {
                Number = "121110987654111",
                Cvv = "321"
            };

            var userToAdd = new User() { Id = "totalyaGuid" };
            var userDiffrentUser = new User();

            using (var context = new BedeThirteenContext(options))
            {
                context.Users.Add(userToAdd);
                context.Users.Add(userDiffrentUser);
                await context.SaveChangesAsync();

                // Act
                var sut = new CreditCardService(context);

                foreach (var card in cardToSeedWith)
                {
                    await sut.AddCreditCardAsync(card.Number, card.Cvv, DateTime.Now, userToAdd.Id);
                }

                await sut.AddCreditCardAsync(cardFromDiffrentUser.Number, cardFromDiffrentUser.Cvv, DateTime.Now, userDiffrentUser.Id);
            }

            // Assert
            using (var context = new BedeThirteenContext(options))
            {
                Assert.IsTrue(context.CreditCards.Any());
                Assert.IsTrue(context.CreditCards.Count() == 4);
                Assert.IsTrue(context.CreditCards.Count(cc => cc.UserId == userToAdd.Id) == 3);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenUser_DoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("ThrowException_WhenUser_DoesNotExist").Options;


            using (var context = new BedeThirteenContext(options))
            {
                // Act
                var sut = new CreditCardService(context);
                await sut.GetUserCardsAsync("invalidId");
            }
        }
    }
}
