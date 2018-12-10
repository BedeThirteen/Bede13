using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using BedeThirteen.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BedeThirteen.Tests.ServicesTests.CreditCardServiceTests
{
    [TestClass]
    public class AddCreditCard_Should
    {
        [TestMethod]
        public async Task AddCard_WhenInputs_AreValid()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("AddCard_WhenInputs_AreValid").Options;

            var CardToAdd = new CreditCard()
            {
                Number = "1234567889101112",
                Cvv = "123",
                UserId = "totalyaGuid"
            };
            var userToAdd = new User();

            using (var contex = new BedeThirteenContext(options))
            {
                contex.Users.Add(userToAdd);
              
                //Act
                var sut = new CreditCardService(contex);                
                await sut.AddCreditCardAsync(CardToAdd.Number, CardToAdd.Cvv, DateTime.Now, userToAdd.Id);
                
            }


            using (var contex = new BedeThirteenContext(options))
            {
                Assert.IsTrue(contex.CreditCards.Any());
                Assert.IsTrue(contex.CreditCards.Any(cc => cc.Number == CardToAdd.Number));
                Assert.IsTrue(contex.CreditCards.Any(cc => cc.Cvv == CardToAdd.Cvv));
                Assert.IsTrue(contex.CreditCards.Any(cc => cc.UserId == userToAdd.Id));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenCardValues_AreNull()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("ThrowException_WhenCardValues_AreNull").Options;

            var CardToAdd = new CreditCard()
            {
               
            };
            var userToAdd = new User();

            using (var contex = new BedeThirteenContext(options))
            {
                contex.Users.Add(userToAdd);

                //Act
                var sut = new CreditCardService(contex);
                await sut.AddCreditCardAsync(CardToAdd.Number, CardToAdd.Cvv, DateTime.Now, userToAdd.Id);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenUser_DoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                .UseInMemoryDatabase("ThrowException_WhenUser_DoesNotExist" ).Options;

            var CardToAdd = new CreditCard()
            {
                Number = "1234567889101112",
                Cvv = "123",
                UserId = "totalyaGuid"
            };
            

            using (var contex = new BedeThirteenContext(options))
            {  
                //Act
                var sut = new CreditCardService(contex);
                await sut.AddCreditCardAsync(CardToAdd.Number, CardToAdd.Cvv, DateTime.Now, CardToAdd.UserId);
            }

 
        }
    }
}
