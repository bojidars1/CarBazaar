using CarBazaar.Server.Controllers;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Notifcations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CarBazaar.UnitTests.Controllers
{
	[TestFixture]
	public class NotificationControllerTests
	{
		private Mock<INotificationService> _notificationServiceMock;
		private NotificationController _controller;

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
			_notificationServiceMock = new Mock<INotificationService>();
			_controller = new NotificationController(_notificationServiceMock.Object);

			var jwtToken = GenerateJwtToken("mock-user-id", "mockuser@example.com");
			AddJwtToContext(jwtToken);
		}

		[Test]
		public async Task GetNotifications_WithValidUserId_ReturnsOk()
		{
			var mockResponse = new NotificationShowPaginatedDto
			{
				Items = new List<NotificationShowDto>
						  {
					new NotificationShowDto
					 {
						Id = Guid.NewGuid().ToString(),
						SenderId = "sender-id",
						CarListingId = "car-listing-id",
						Message = "Test Notification 1",
						IsRead = false
					  },
					new NotificationShowDto
					{
			          Id = Guid.NewGuid().ToString(),
			          SenderId = "sender-id-2",
			          CarListingId = "car-listing-id-2",
			          Message = "Test Notification 2",
			          IsRead = true
		            }
	            },
				TotalPages = 1
			};

			_notificationServiceMock
				.Setup(s => s.GetNotificationsAsync("mock-user-id", 1, 10))
				.ReturnsAsync(mockResponse);

			var result = await _controller.GetNotifications();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
			var okResult = result as OkObjectResult;
			Assert.That(okResult.Value, Is.EqualTo(mockResponse));
		}

		[Test]
		public async Task MarkAsReadAsync_WithValidIds_ReturnsOk()
		{
			var notificationIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

			_notificationServiceMock.Setup(s => s.MarkNotificationsAsReadAsync(notificationIds))
				.Returns(Task.CompletedTask);

			var result = await _controller.MarkAsReadAsync(notificationIds);

			Assert.That(result, Is.InstanceOf<OkResult>());
		}

		[Test]
		public async Task Delete_WithValidId_ReturnsOk()
		{
			_notificationServiceMock.Setup(s => s.DeleteAsync("mock-user-id", "notification123"))
				.ReturnsAsync(true);

			var result = await _controller.Delete("notification123");

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task Delete_WithInvalidId_ReturnsBadRequest()
		{
			_notificationServiceMock.Setup(s => s.DeleteAsync("mock-user-id", "notification123"))
				.ReturnsAsync(false);

			var result = await _controller.Delete("notification123");

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}
	}
}