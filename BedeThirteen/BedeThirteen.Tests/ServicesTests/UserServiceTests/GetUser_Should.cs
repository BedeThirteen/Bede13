namespace BedeThirteen.Tests.ServicesTests.UserServiceTests
{
    using System;
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
    public class GetUser_Should
    {
        [TestMethod]
        public async Task ReturnUser_WhenValidIdIsPassed()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
                       .UseInMemoryDatabase($"ReturnUser_WhenValidIdIsPassed").Options;

            var id = Guid.NewGuid().ToString();
            var mockCurrency = new Currency() { Id = Guid.NewGuid(), Name = "FOO" };
            User userToAdd = new User() { Id = id };

            using (var context = new BedeThirteenContext(options))
            {
                context.Currencies.Add(mockCurrency);
                userToAdd.CurrencyId = mockCurrency.Id;
                context.Users.Add(userToAdd);
                context.SaveChanges();
            }

            // Act
            User result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new UserService(context, this.SetupUserManagerMock().Object);
                result = await sut.GetUserAsync(id);
            }

            // Assert
            Assert.AreEqual(userToAdd.Id, result.Id);
        }

        [TestMethod]
        public async Task ReturnNull_WhenNotFound()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
              .UseInMemoryDatabase($"ReturnNull_WhenNotFound").Options;

            // Act
            var invalidId = Guid.NewGuid().ToString();
            User result;
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new UserService(context, this.SetupUserManagerMock().Object);
                result = await sut.GetUserAsync(invalidId);
            }

            // Assert
            Assert.IsNull(result);
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
