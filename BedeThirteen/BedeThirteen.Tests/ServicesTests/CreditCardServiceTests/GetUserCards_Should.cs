namespace BedeThirteen.Tests.ServicesTests.CreditCardServiceTests
{
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
    public class GetUserCards_Should
    {
        [TestMethod]
        public async Task GetCards_WhenInputs_AreValid()
        {
            // Arrange
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

            var userToAdd = new User() { Id = "totalyaGuid" };
            var userDiffrentUser = new User();

            using (var context = new BedeThirteenContext(options))
            {
                context.Users.Add(userToAdd);
                context.Users.Add(userDiffrentUser);
                await context.SaveChangesAsync();

                foreach (var card in cardToSeedWith)
                {
                    card.UserId = userToAdd.Id;
                    context.CreditCards.Add(card);
                }

                await context.SaveChangesAsync();
            }

            // Act
            ICollection<CreditCard> results;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new CreditCardService(context);
                results = await sut.GetUserCardsAsync(userToAdd.Id);
            }

            // Assert
            Assert.IsTrue(results.Any());
            Assert.IsTrue(results.Count() == 3);
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
