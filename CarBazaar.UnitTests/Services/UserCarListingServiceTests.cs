using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Extensions;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.UnitTests.Services
{
	[TestFixture]
	public class UserCarListingServiceTests
	{
		private Mock<IUserCarListingRepository> _repoMock;
		private UserCarListingService _service;

		[SetUp]
		public void Setup()
		{
			_repoMock = new Mock<IUserCarListingRepository>();
			_service = new UserCarListingService(_repoMock.Object);
		}

		[Test]
		public async Task AddAsync_ShouldCallAddOnRepository()
		{
			var listing = new UserCarListing { UserId = "user123", CarListingId = Guid.NewGuid() };
			await _service.AddAsync(listing);

			_repoMock.Verify(r => r.AddAsync(listing), Times.Once);
		}

		[Test]
		public async Task GetByCarIdAsync_ReturnsExpectedListing()
		{
			var carId = Guid.NewGuid().ToString();
			var expected = new UserCarListing { CarListingId = Guid.Parse(carId), UserId = "UserX" };
			_repoMock.Setup(r => r.GetByCarIdAsync(carId)).ReturnsAsync(expected);

			var result = await _service.GetByCarIdAsync(carId);

			result.Should().NotBeNull();
			result!.CarListingId.ToString().Should().Be(carId);
		}

		[Test]
		public async Task GetByCarIdAsync_NotFound_ReturnsNull()
		{
			_repoMock.Setup(r => r.GetByCarIdAsync(It.IsAny<string>())).ReturnsAsync((UserCarListing)null);

			var result = await _service.GetByCarIdAsync(Guid.NewGuid().ToString());
			result.Should().BeNull();
		}

		[Test]
		public async Task GetByUserIdAsync_ReturnsExpectedListing()
		{
			var userId = "UserABC";
			var expected = new UserCarListing { UserId = userId, CarListingId = Guid.NewGuid() };
			_repoMock.Setup(r => r.GetByUserIdAsync(userId)).ReturnsAsync(expected);

			var result = await _service.GetByUserIdAsync(userId);

			result.Should().NotBeNull();
			result!.UserId.Should().Be(userId);
		}

		[Test]
		public async Task GetByUserIdAsync_NotFound_ReturnsNull()
		{
			_repoMock.Setup(r => r.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync((UserCarListing)null);

			var result = await _service.GetByUserIdAsync("NonUser");
			result.Should().BeNull();
		}

		[Test]
		public async Task GetListingsAsync_NullOrEmptyUserId_ReturnsNull()
		{
			var res1 = await _service.GetListingsAsync(null!);
			var res2 = await _service.GetListingsAsync("");

			res1.Should().BeNull();
			res2.Should().BeNull();
		}

		[Test]
		public async Task GetListingsAsync_ValidUserId_ReturnsPaginatedDto()
		{
			var userId = "User123";
			var carId = Guid.NewGuid();
			var userCarListings = new List<UserCarListing>
		{
			new UserCarListing
			{
				UserId = userId,
				CarListingId = carId,
				CarListing = new CarListing { Id = carId, Name = "CarName", Price = 20000, ImageURL = "some image" }
			}
		};

			var paginated = new PaginatedList<UserCarListing>(userCarListings, userCarListings.Count, 1, 10);

			_repoMock.Setup(r => r.GetBaseQuery()).Returns(userCarListings.AsQueryable());

			_repoMock.Setup(r => r.GetPaginatedAsync(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<IQueryable<UserCarListing>>()))
					 .ReturnsAsync(paginated);

			var result = await _service.GetListingsAsync(userId, 1, 10);

			result.Should().NotBeNull();
			result!.Items.Should().HaveCount(1);
			result.Items[0].Name.Should().Be("CarName");
			result.TotalPages.Should().Be(1);
		}

		[Test]
		public async Task GetListingsAsync_NoListings_ReturnsEmptyDto()
		{
			var userId = "UserEmpty";
			var emptyList = new List<UserCarListing>();
			var paginated = new PaginatedList<UserCarListing>(emptyList, 0, 1, 10);

			_repoMock.Setup(r => r.GetBaseQuery()).Returns(emptyList.AsQueryable());
			_repoMock.Setup(r => r.GetPaginatedAsync(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<IQueryable<UserCarListing>>()))
					 .ReturnsAsync(paginated);

			var result = await _service.GetListingsAsync(userId, 1, 10);

			result.Should().NotBeNull();
			result!.Items.Should().BeEmpty();
			result.TotalPages.Should().Be(0);
		}
	}
}