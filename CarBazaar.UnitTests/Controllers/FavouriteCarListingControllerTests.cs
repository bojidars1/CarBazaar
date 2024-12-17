using CarBazaar.Server.Controllers;
using CarBazaar.Services.Contracts;
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
	public class FavouriteCarListingControllerTests
	{
		private Mock<IFavouriteCarListingService> _favouriteServiceMock;
		private FavouriteCarListingController _controller;

		private string GenerateJwtToken(string userId, string email)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My Super Secret Key For JWT for my Application"));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId),
				new Claim(JwtRegisteredClaimNames.Email, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			var token = new JwtSecurityToken(
				issuer: "TestIssuer",
				audience: "TestAudience",
				claims: claims,
				expires: DateTime.UtcNow.AddHours(1),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private void AddJwtToContext(string token)
		{
			_controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity(new JwtSecurityTokenHandler()
						.ReadJwtToken(token).Claims, "Bearer")),
					Request =
					{
						Headers =
						{
							["Authorization"] = $"Bearer {token}"
						}
					}
				}
			};
		}

		[SetUp]
		public void Setup()
		{
			_favouriteServiceMock = new Mock<IFavouriteCarListingService>();
			_controller = new FavouriteCarListingController(_favouriteServiceMock.Object);

			var jwtToken = GenerateJwtToken("mock-user-id", "mockuser@example.com");
			AddJwtToContext(jwtToken);
		}

		[Test]
		public async Task AddToFavourite_ValidRequest_ReturnsOk()
		{
			_favouriteServiceMock.Setup(s => s.AddToFavouriteAsync("car123", "mock-user-id"))
				.ReturnsAsync(true);

			var result = await _controller.AddToFavourite("car123");

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
			_favouriteServiceMock.Verify(s => s.AddToFavouriteAsync("car123", "mock-user-id"), Times.Once);
		}

		[Test]
		public async Task AddToFavourite_FailedToAdd_ReturnsBadRequest()
		{
			_favouriteServiceMock.Setup(s => s.AddToFavouriteAsync("car123", "mock-user-id"))
				.ReturnsAsync(false);

			var result = await _controller.AddToFavourite("car123");

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
			_favouriteServiceMock.Verify(s => s.AddToFavouriteAsync("car123", "mock-user-id"), Times.Once);
		}

		[Test]
		public async Task RemoveFromFavourite_ValidRequest_ReturnsOk()
		{
			_favouriteServiceMock.Setup(s => s.DeleteFavouriteAsync("car123", "mock-user-id"))
				.ReturnsAsync(true);

			var result = await _controller.RemoveFromFavourite("car123");

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
			_favouriteServiceMock.Verify(s => s.DeleteFavouriteAsync("car123", "mock-user-id"), Times.Once);
		}

		[Test]
		public async Task RemoveFromFavourite_FailedToDelete_ReturnsBadRequest()
		{
			_favouriteServiceMock.Setup(s => s.DeleteFavouriteAsync("car123", "mock-user-id"))
				.ReturnsAsync(false);

			var result = await _controller.RemoveFromFavourite("car123");

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
			_favouriteServiceMock.Verify(s => s.DeleteFavouriteAsync("car123", "mock-user-id"), Times.Once);
		}
	}
}