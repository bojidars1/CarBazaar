using CarBazaar.Data.Models;
using CarBazaar.Server.Controllers;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Chat;
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
	public class ChatControllerTests
	{
		private Mock<IChatService> _chatServiceMock;
		private Mock<INotificationService> _notificationServiceMock;
		private ChatController _controller;

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
			_chatServiceMock = new Mock<IChatService>();
			_notificationServiceMock = new Mock<INotificationService>();
			_controller = new ChatController(_chatServiceMock.Object, _notificationServiceMock.Object);

			var jwtToken = GenerateJwtToken("mock-user-id", "mockuser@example.com");
			AddJwtToContext(jwtToken);
		}

		[Test]
		public async Task SendMessage_ValidRequest_ReturnsOk()
		{
			var request = new SendMessageRequest
			{
				ReceiverId = "receiver-id",
				CarListingId = Guid.NewGuid(),
				Message = "Hello!"
			};

			_chatServiceMock.Setup(cs => cs.IsOneOfThemOwner("mock-user-id", request.ReceiverId, request.CarListingId.ToString()))
				.ReturnsAsync(true);

			var result = await _controller.SendMessage(request);

			Assert.That(result is OkObjectResult);
			_chatServiceMock.Verify(cs => cs.SendMessageAsync("mock-user-id", request.ReceiverId, request.CarListingId, request.Message), Times.Once);
			_notificationServiceMock.Verify(ns => ns.AddNotificationAsync(It.IsAny<Notification>()), Times.Once);
		}

		[Test]
		public async Task SendMessage_InvalidUserId_ReturnsBadRequest()
		{
			var request = new SendMessageRequest
			{
				ReceiverId = "mock-user-id",
				CarListingId = Guid.NewGuid(),
				Message = "Invalid test"
			};

			var result = await _controller.SendMessage(request);

			Assert.That(result is BadRequestObjectResult);
			_chatServiceMock.Verify(cs => cs.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
		}
	}
}