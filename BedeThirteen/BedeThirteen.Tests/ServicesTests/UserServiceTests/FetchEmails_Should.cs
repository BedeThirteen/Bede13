namespace BedeThirteen.Tests.ServicesTests.UserServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class FetchEmails_Should
    {
        [TestMethod]
        public async Task ReturnListOfStrings_WhenEntitiesArePresent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                       .UseInMemoryDatabase($"ReturnUser_WhenValidIdIsPassed").Options;

            var idOne = Guid.NewGuid().ToString();
            User userOne = new User()
            {
                Id = idOne,
                Email = "emailOne",
                NormalizedEmail = "EMAILONE"
            };
            var idTwo = Guid.NewGuid().ToString();
            User userTwo = new User()
            {
                Id = idTwo,
                Email = "emailTwo",
                NormalizedEmail = "EMAILTWO"
            };

            using (var context = new BedeThirteenContext(options))
            {
                context.Users.Add(userOne);
                context.Users.Add(userTwo);
                context.SaveChanges();
            }

            // Act
            List<string> results = new List<string>();
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new UserService(context, this.SetupUserManagerMock().Object);
                var data = await sut.FetchEmailsAsync("EMAIL");
                results = data.ToList();
            }

            // Assert
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(true, results.Contains("emailOne"));
            Assert.AreEqual(true, results.Contains("emailTwo"));
        }

        private Mock<UserManager<User>> SetupUserManagerMock()
        {
            return new Mock<UserManager<User>>(
                  new Mock<IUserStore<User>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<User>>().Object,
                  new IUserValidator<User>[0],
                  new IPasswordValidator<User>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<User>>>().Object);
        }
    }
}