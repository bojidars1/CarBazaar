using CarBazaar.Server.Controllers;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.CarListing;
using Microsoft.AspNetCore.Http;
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
	public class UserCarListingControllerTests
	{
		private Mock<IUserCarListingService> _userCarListingServiceMock;
		private UserCarListingController _controller;

		[SetUp]
		public void SetUp()
		{
			_userCarListingServiceMock = new Mock<IUserCarListingService>();
			_controller = new UserCarListingController(_userCarListingServiceMock.Object);
		}

		private void SetUserContext(string userId)
		{
			var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
			var identity = new ClaimsIdentity(claims, "mock");
			var principal = new ClaimsPrincipal(identity);

			var context = new DefaultHttpContext
			{
				User = principal
			};

			_controller.ControllerContext = new ControllerContext
			{
				HttpContext = context
			};
		}
	}
}