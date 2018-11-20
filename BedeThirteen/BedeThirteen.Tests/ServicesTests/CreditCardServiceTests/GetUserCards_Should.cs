using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.CreditCardServiceTests
{   
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
            }};

            var cardFromDiffrentUser = new CreditCard()
            {
                Number = "121110987654111",
                Cvv = "321"
            };

            var userToAdd = new User();
            var userDiffrentUser = new User();


            using (var contex = new BedeThirteenContext(options))
            {
                contex.Users.Add(userToAdd);
                contex.Users.Add(userDiffrentUser);


                //Act
                var sut = new CreditCardService(contex);

                foreach (var card in cardToSeedWith)
                {
                    await sut.AddCreditCardAsync(card.Number, card.Cvv, DateTime.Now, userToAdd.Id);
                }

                await sut.AddCreditCardAsync(cardFromDiffrentUser.Number, cardFromDiffrentUser.Cvv, DateTime.Now, userDiffrentUser.Id);
            }


            using (var contex = new BedeThirteenContext(options))
            {

                Assert.IsTrue(contex.CreditCards.Any());
                Assert.IsTrue(contex.CreditCards.Count() ==4);
                Assert.IsTrue(contex.CreditCards.Count(cc => cc.UserId == userToAdd.Id) == 3);
            }
        }
    }
}
