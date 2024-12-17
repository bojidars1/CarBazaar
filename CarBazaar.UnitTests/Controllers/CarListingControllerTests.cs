using CarBazaar.Server.Controllers;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.CarListing;
using FluentAssertions;
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
	public class CarListingControllerTests
	{
		private Mock<ICarListingService> _serviceMock;
		private CarListingController _controller;

		[SetUp]
		public void Setup()
		{
			_serviceMock = new Mock<ICarListingService>();
			_controller = new CarListingController(_serviceMock.Object);

			var httpContext = new DefaultHttpContext();
			string jwtToken = GenerateMockJwtToken();
			httpContext.Request.Headers["Authorization"] = $"Bearer {jwtToken}";
			_controller.ControllerContext = new ControllerContext
			{
				HttpContext = httpContext
			};
		}

		[Test]
		public async Task GetListings_ReturnsOkResult_WithPaginatedListings()
		{
			var paginatedResult = new CarListingPaginatedSearchDto
			{
				Items = new List<CarListingListDetailsDto>
				{
					new CarListingListDetailsDto { Id = Guid.NewGuid(), Name = "Car 1", Price = 20000, ImageURL = "url1" },
					new CarListingListDetailsDto { Id = Guid.NewGuid(), Name = "Car 2", Price = 30000, ImageURL = "url2" }
				},
				TotalPages = 2
			};

			_serviceMock.Setup(s => s.GetPaginatedCarListingsAsync(1, 10)).ReturnsAsync(paginatedResult);

			var result = await _controller.GetListings(1, 10);

			var okResult = result as OkObjectResult;
			okResult.Should().NotBeNull();
			okResult.StatusCode.Should().Be(200);
			var value = okResult.Value as CarListingPaginatedSearchDto;
			value.Should().BeEquivalentTo(paginatedResult);
		}

		[Test]
		public async Task Details_WithValidId_ReturnsOkResult()
		{
			var carDetails = new CarListingDetailsDto { Id = Guid.NewGuid(), Name = "Car 1", Price = 20000 };
			_serviceMock.Setup(s => s.GetCarListingDetailsByIdAsync(It.IsAny<string>())).ReturnsAsync(carDetails);

			var result = await _controller.Details("valid-id");

			var okResult = result as OkObjectResult;
			okResult.Should().NotBeNull();
			okResult.StatusCode.Should().Be(200);
			okResult.Value.Should().BeEquivalentTo(carDetails);
		}

		[Test]
		public async Task Details_WithInvalidId_ReturnsNotFound()
		{
			_serviceMock.Setup(s => s.GetCarListingDetailsByIdAsync(It.IsAny<string>())).ReturnsAsync((CarListingDetailsDto)null);

			var result = await _controller.Details("invalid-id");

			var notFoundResult = result as NotFoundResult;
			notFoundResult.Should().NotBeNull();
			notFoundResult.StatusCode.Should().Be(404);
		}

		[Test]
		public async Task Add_WithValidData_ReturnsOkResult()
		{
			var dto = new AddCarListingDto { Name = "New Car", Price = 15000 };

			_controller.ControllerContext.HttpContext.Items["UserId"] = "mock-user-id";
			_serviceMock.Setup(s => s.AddAsync(dto, "mock-user-id")).Returns(Task.CompletedTask);

			var result = await _controller.Add(dto);

			var okResult = result as OkObjectResult;
			okResult.Should().NotBeNull();
			okResult.StatusCode.Should().Be(200);
			okResult.Value.Should().Be("Success");
		}

		[Test]
		public async Task Edit_WithValidData_ReturnsOkResult()
		{
			var dto = new EditCarListingDto { Id = "1", Name = "Updated Car" };
			_serviceMock.Setup(s => s.UpdateCarListingAsync(dto, It.IsAny<string>())).ReturnsAsync(true);

			_controller.ControllerContext.HttpContext.Items["UserId"] = "mock-user-id";

			var result = await _controller.Edit(dto);

			var okResult = result as OkObjectResult;
			okResult.Should().NotBeNull();
			okResult.StatusCode.Should().Be(200);
		}

		[Test]
		public async Task Edit_WithInvalidData_ReturnsNotFound()
		{
			var dto = new EditCarListingDto();
			_serviceMock.Setup(s => s.UpdateCarListingAsync(dto, It.IsAny<string>())).ReturnsAsync(false);

			_controller.ControllerContext.HttpContext.Items["UserId"] = "mock-user-id";

			var result = await _controller.Edit(dto);

			var notFoundResult = result as NotFoundResult;
			notFoundResult.Should().NotBeNull();
		}

		[Test]
		public async Task SoftDelete_WithValidId_ReturnsOk()
		{
			_serviceMock.Setup(s => s.SoftDeleteCarListingAsync("valid-id", "mock-user-id")).ReturnsAsync(true);

			_controller.ControllerContext.HttpContext.Items["UserId"] = "mock-user-id";

			var result = await _controller.SoftDelete("valid-id");

			var okResult = result as OkObjectResult;
			okResult.Should().NotBeNull();
			okResult.StatusCode.Should().Be(200);
		}

		[Test]
		public async Task SoftDelete_WithInvalidId_ReturnsNotFound()
		{
			_serviceMock.Setup(s => s.SoftDeleteCarListingAsync("invalid-id", "mock-user-id")).ReturnsAsync(false);

			_controller.ControllerContext.HttpContext.Items["UserId"] = "mock-user-id";

			var result = await _controller.SoftDelete("invalid-id");

			var notFoundResult = result as NotFoundResult;
			notFoundResult.Should().NotBeNull();
			notFoundResult.StatusCode.Should().Be(404);
		}

		private string GenerateMockJwtToken()
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My Super Secret Key For JWT for my Application"));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
		     new Claim(JwtRegisteredClaimNames.Sub, "mock-user-id"),
		     new Claim(JwtRegisteredClaimNames.Email, "mock@example.com"),
	        };

			var token = new JwtSecurityToken(
				issuer: "TestIssuer",
				audience: "TestAudience",
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}