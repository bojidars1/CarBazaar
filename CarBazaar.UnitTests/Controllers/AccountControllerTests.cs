using CarBazaar.Data.Models;
using CarBazaar.Server.Controllers;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarBazaar.UnitTests.Controllers
{
	[TestFixture]
	public class AccountControllerTests
	{
		private Mock<IJwtService> _jwtServiceMock;
		private Mock<IRedisService> _redisServiceMock;
		private Mock<UserManager<CarBazaarUser>> _userManagerMock;
		private AccountController _controller;

		[SetUp]
		public void SetUp()
		{
			_jwtServiceMock = new Mock<IJwtService>();
			_redisServiceMock = new Mock<IRedisService>();
			_userManagerMock = MockUserManager();

			_controller = new AccountController(
				_jwtServiceMock.Object,
				_redisServiceMock.Object,
				_userManagerMock.Object
			);
		}

		private Mock<UserManager<CarBazaarUser>> MockUserManager()
		{
			var store = new Mock<IUserStore<CarBazaarUser>>();
			return new Mock<UserManager<CarBazaarUser>>(store.Object, null, null, null, null, null, null, null, null);
		}

		[Test]
		public async Task Register_WithInvalidData_ReturnsBadRequest()
		{
			var registerDto = new RegisterDto { Email = "test@example.com", Password = "Password123!" };

			_userManagerMock.Setup(um => um.CreateAsync(It.IsAny<CarBazaarUser>(), registerDto.Password))
							.ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

			var result = await _controller.Register(registerDto);

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}

		[Test]
		public async Task Login_WithValidCredentials_ReturnsOkWithAccessToken()
		{
			var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123!" };
			var user = new CarBazaarUser { Id = "1", Email = loginDto.Email };

			_userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email))
							.ReturnsAsync(user);
			_userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginDto.Password))
							.ReturnsAsync(true);
			_jwtServiceMock.Setup(j => j.GenerateAccessToken(user.Id, user.Email))
						   .ReturnsAsync("mock-token");

			var result = await _controller.Login(loginDto);

			var okResult = result as OkObjectResult;
			Assert.That(okResult, Is.Not.Null);
			Assert.That(okResult.StatusCode, Is.EqualTo(200));
			var accessToken = okResult.Value.GetType().GetProperty("accessToken").GetValue(okResult.Value, null);
			Assert.That(accessToken, Is.EqualTo("mock-token"));
		}

		[Test]
		public async Task Login_WithInvalidCredentials_ReturnsBadRequest()
		{
			var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123!" };

			_userManagerMock.Setup(um => um.FindByEmailAsync(loginDto.Email))
							.ReturnsAsync((CarBazaarUser)null);

			var result = await _controller.Login(loginDto);

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}

		[Test]
		public async Task Logout_WithNoToken_ReturnsBadRequest()
		{
			var context = new Mock<HttpContext>();
			context.Setup(h => h.Request.Headers["Authorization"]).Returns(string.Empty);
			_controller.ControllerContext.HttpContext = context.Object;

			var result = await _controller.Logout();

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}
	}
}