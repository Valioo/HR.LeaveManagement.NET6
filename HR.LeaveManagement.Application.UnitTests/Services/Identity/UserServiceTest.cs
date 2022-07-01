using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.DTOs.Employee;
using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagement.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
namespace HR.LeaveManagement.Application.UnitTests.Services.Identity
{
    public class UserServiceTest
    {
        private const string _id = "sameRandomId";
        private const string _firstName = "Pesho";
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<IAuthService> _mockAuthService;
        private Mock<IMapper> _mockMapper;
        private ApplicationUser user = new ApplicationUser { Id = _id, FirstName = _firstName };
        public UserServiceTest()
        {
            this._mockUserManager = MockUserManager(user);
            this._mockAuthService = new Mock<IAuthService>();
            this._mockMapper = new Mock<IMapper>();
        }
        [Fact]
        public async Task GetEmployeeById_ShouldReturnCorectData_WhenIdExists()
        {
            _mockMapper.Setup(x => x.Map<EmployeeDetailsDto>(It.IsAny<ApplicationUser>())).Returns(new EmployeeDetailsDto { FirstName = _firstName });
            var userService = new UserService(_mockUserManager.Object, _mockAuthService.Object, _mockMapper.Object);
            var result = await userService.GetEmployeeById(_id);
            Assert.Equal(_firstName, result.FirstName);
        }
        public static Mock<UserManager<ApplicationUser>> MockUserManager(ApplicationUser user)
        {
			//
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Setup(x => x.FindByIdAsync(user.Id)).ReturnsAsync(user);
            mgr.Setup(x => x.Users).Returns(() => (new List<ApplicationUser>()).AsQueryable<ApplicationUser>());
            return mgr;
        }
        private static UserManager<ApplicationUser> CreateUserManager()
        {
            Mock<IUserPasswordStore<ApplicationUser>> userPasswordStore = new Mock<IUserPasswordStore<ApplicationUser>>();
            userPasswordStore.Setup(s => s.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            //this should be keep in sync with settings in ConfigureIdentity in WebApi -> Startup.cs
            idOptions.Lockout.AllowedForNewUsers = false;
            idOptions.Password.RequireDigit = true;
            idOptions.Password.RequireLowercase = true;
            idOptions.Password.RequireNonAlphanumeric = true;
            idOptions.Password.RequireUppercase = true;
            idOptions.Password.RequiredLength = 6;
            idOptions.SignIn.RequireConfirmedEmail = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<ApplicationUser>>();
            UserValidator<ApplicationUser> validator = new UserValidator<ApplicationUser>();
            userValidators.Add(validator);
            var passValidator = new PasswordValidator<ApplicationUser>();
            var pwdValidators = new List<IPasswordValidator<ApplicationUser>>();
            pwdValidators.Add(passValidator);
            var userManager = new UserManager<ApplicationUser>(userPasswordStore.Object, options.Object, new PasswordHasher<ApplicationUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            return userManager;
        }
    }
}