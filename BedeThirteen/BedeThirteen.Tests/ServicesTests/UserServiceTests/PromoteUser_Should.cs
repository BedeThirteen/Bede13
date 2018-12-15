namespace BedeThirteen.Tests.ServicesTests.UserServiceTests
{
    using System;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Services;
    using BedeThirteen.Services.Exceptions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class PromoteUser_Should
    {
        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenInvalidEmailIsPassed()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
             .UseInMemoryDatabase($"ThrowException_WhenInvalidEmailIsPassed").Options;

            // Arrange
            var id = Guid.NewGuid().ToString();
            User userToAdd = new User() { Email = "a@a.com", NormalizedEmail = "A@A.COM" };

            using (var context = new BedeThirteenContext(options))
            {
                context.Users.Add(userToAdd);
                context.SaveChanges();
            }

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new UserService(context, this.SetupUserManagerMock().Object);
                var result = await sut.PromoteUserAsync("a@a.com");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task ThrowException_WhenUserIsWithInvalidRole()
        {
            var options = new DbContextOptionsBuilder<BedeThirteenContext>()
             .UseInMemoryDatabase($"ThrowException_WhenInvalidEmailIsPassed").Options;

            // Arrange
            var id = Guid.NewGuid().ToString();
            var userRole = new IdentityRole()
            {
                Id = "userRoleId",
                NormalizedName = "USER"
            };

            User userToAdd = new User() { Email = "a@a.com", NormalizedEmail = "A@A.COM" };

            using (var context = new BedeThirteenContext(options))
            {
                context.Roles.Add(userRole);
                context.Users.Add(userToAdd);
                context.SaveChanges();

                context.UserRoles.Add(new IdentityUserRole<string>()
                {
                    RoleId = userRole.Id,
                    UserId = userToAdd.Id
                });
            }

            // Act
            using (var context = new BedeThirteenContext(options))
            {
                var sut = new UserService(context, this.SetupUserManagerMock().Object);
                var result = await sut.PromoteUserAsync("a@a.com");
            }
        }

        //[TestMethod]
        //public async Task ChaneRoleToAdmin_WhenDataIsValid()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<BedeThirteenContext>()
        //     .UseInMemoryDatabase($"ChaneRoleToAdmin_WhenDataIsValid").Options;

        // // Arrange
        //    var id = Guid.NewGuid().ToString();
        //    var userRole = new IdentityRole()
        //    {
        //        Id = "userRoleId",
        //        NormalizedName = "USER"
        //    };
        //    var adminRole = new IdentityRole()
        //    {
        //        Id = "adminRoleId",
        //        NormalizedName = "ADMIN"
        //    };

        //    User userToAdd = new User() { Email = "a@a.com", NormalizedEmail = "A@A.COM" };

        //    using (var context = new BedeThirteenContext(options))
        //    {
        //        context.Roles.Add(userRole);
        //        context.Roles.Add(adminRole);
        //        context.Users.Add(userToAdd);
        //        context.SaveChanges();

        //        context.UserRoles.Add(new IdentityUserRole<string>()
        //        {
        //            RoleId = userRole.Id,
        //            UserId = userToAdd.Id
        //        });
        //    }

        // // Act
        //    using (var context = new BedeThirteenContext(options))
        //    {
        //        var sut = new UserService(context, this.SetupUserManagerMock().Object);
        //    }

        // // Assert
        //    using (var context = new BedeThirteenContext(options))
        //    {
        //        var resultRoleId = (await context.UserRoles.FirstOrDefaultAsync()).RoleId;
        //        Assert.AreEqual(adminRole.Id, resultRoleId);
        //    }
        //}

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
