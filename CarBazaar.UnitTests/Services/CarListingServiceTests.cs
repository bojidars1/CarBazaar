using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Extensions;
using CarBazaar.Infrastructure.Repositories;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services;
using CarBazaar.Services.Contracts;
using CarBazaar.UnitTests.Common;
using CarBazaar.ViewModels.CarListing;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace CarBazaar.UnitTests.Services
{
	[TestFixture]
	public class CarListingServiceTests
	{
		private Mock<ICarListingRepository> _repoMock;
		private Mock<IUserCarListingService> _userCarListingServiceMock;
		private Mock<UserManager<CarBazaarUser>> _userManagerMock;
		private CarListingService _service;

		[SetUp]
		public void Setup()
		{
			_repoMock = new Mock<ICarListingRepository>();
			_userCarListingServiceMock = new Mock<IUserCarListingService>();

			var store = new Mock<IUserStore<CarBazaarUser>>();
			_userManagerMock = new Mock<UserManager<CarBazaarUser>>(
				store.Object, null, null, null, null, null, null, null, null
			);

			_service = new CarListingService(_repoMock.Object, _userCarListingServiceMock.Object, _userManagerMock.Object);
		}

		[Test]
		public async Task AddAsync_ShouldAddCarListingAndUserCarListing()
		{
			var dto = new AddCarListingDto { Name = "TestCar", Type = "SUV", Brand = "BMW", Price = 20000 };
			var userId = "user123";

			await _service.AddAsync(dto, userId);

			_repoMock.Verify(r => r.AddAsync(It.Is<CarListing>(c => c.Name == "TestCar")), Times.Once);
			_userCarListingServiceMock.Verify(u => u.AddAsync(It.Is<UserCarListing>(uc => uc.UserId == userId)), Times.Once);
		}

		[Test]
		public async Task UpdateCarListingAsync_UserNotFound_ReturnsFalse()
		{
			_userManagerMock.Setup(u => u.FindByIdAsync("user123")).ReturnsAsync((CarBazaarUser)null);
			var dto = new EditCarListingDto { Id = Guid.NewGuid().ToString() };
			var result = await _service.UpdateCarListingAsync(dto, "user123");
			result.Should().BeFalse();
		}

		[Test]
		public async Task UpdateCarListingAsync_NotAdminAndNotOwner_ReturnsFalse()
		{
			var user = new CarBazaarUser { Id = "user123" };
			_userManagerMock.Setup(u => u.FindByIdAsync("user123")).ReturnsAsync(user);
			_userManagerMock.Setup(u => u.IsInRoleAsync(user, "Administrator")).ReturnsAsync(false);

			_userCarListingServiceMock.Setup(s => s.GetByCarIdAsync(It.IsAny<string>()))
				.ReturnsAsync((UserCarListing)null);

			var dto = new EditCarListingDto { Id = Guid.NewGuid().ToString() };
			var result = await _service.UpdateCarListingAsync(dto, "user123");
			result.Should().BeFalse();
		}

		[Test]
		public async Task UpdateCarListingAsync_ValidData_ReturnsTrueAndUpdates()
		{
			var user = new CarBazaarUser { Id = "adminUser" };
			_userManagerMock.Setup(u => u.FindByIdAsync("adminUser")).ReturnsAsync(user);
			_userManagerMock.Setup(u => u.IsInRoleAsync(user, "Administrator")).ReturnsAsync(true);

			var listingId = Guid.NewGuid();
			var listing = new CarListing { Id = listingId, Name = "OldName" };
			_repoMock.Setup(r => r.GetByIdAsync(listingId.ToString())).ReturnsAsync(listing);

			var dto = new EditCarListingDto { Id = listingId.ToString(), Name = "NewName" };
			var result = await _service.UpdateCarListingAsync(dto, "adminUser");

			result.Should().BeTrue();
			listing.Name.Should().Be("NewName");
			_repoMock.Verify(r => r.UpdateAsync(listing), Times.Once);
		}

		[Test]
		public async Task GetAllAsync_ReturnsListOfDto()
		{
			var listings = new List<CarListing> {
			new CarListing { Id = Guid.NewGuid(), Name = "Car1", Price = 1000 },
			new CarListing { Id = Guid.NewGuid(), Name = "Car2", Price = 2000 }
		};
			_repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(listings);

			var result = await _service.GetAllAsync();

			result.Should().HaveCount(2);
			result[0].Name.Should().Be("Car1");
			result[1].Price.Should().Be(2000);
		}

		[Test]
		public async Task GetCarListingDetailsByIdAsync_NotFound_ReturnsNull()
		{
			_repoMock.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((CarListing)null);
			var result = await _service.GetCarListingDetailsByIdAsync(Guid.NewGuid().ToString());
			result.Should().BeNull();
		}

		[Test]
		public async Task GetCarListingDetailsByIdAsync_Found_ReturnsDto()
		{
			var listing = new CarListing { Id = Guid.NewGuid(), Name = "DetailCar", Type = "Sedan" };
			_repoMock.Setup(r => r.GetByIdAsync(listing.Id.ToString())).ReturnsAsync(listing);

			var result = await _service.GetCarListingDetailsByIdAsync(listing.Id.ToString());
			result.Should().NotBeNull();
			result!.Name.Should().Be("DetailCar");
			result.Type.Should().Be("Sedan");
		}

		[Test]
		public async Task SoftDeleteCarListingAsync_UserNotFound_ReturnsFalse()
		{
			_userManagerMock.Setup(u => u.FindByIdAsync("userX")).ReturnsAsync((CarBazaarUser)null);
			var result = await _service.SoftDeleteCarListingAsync(Guid.NewGuid().ToString(), "userX");
			result.Should().BeFalse();
		}

		[Test]
		public async Task SoftDeleteCarListingAsync_NotAdminNotOwner_ReturnsFalse()
		{
			var user = new CarBazaarUser { Id = "userABC" };
			_userManagerMock.Setup(u => u.FindByIdAsync("userABC")).ReturnsAsync(user);
			_userManagerMock.Setup(u => u.IsInRoleAsync(user, "Administrator")).ReturnsAsync(false);

			_userCarListingServiceMock.Setup(s => s.GetByCarIdAsync(It.IsAny<string>()))
				.ReturnsAsync((UserCarListing)null);

			var result = await _service.SoftDeleteCarListingAsync(Guid.NewGuid().ToString(), "userABC");
			result.Should().BeFalse();
		}

		[Test]
		public async Task SoftDeleteCarListingAsync_FoundAndAllowed_DeletesAndReturnsTrue()
		{
			var user = new CarBazaarUser { Id = "adminUser" };
			_userManagerMock.Setup(u => u.FindByIdAsync("adminUser")).ReturnsAsync(user);
			_userManagerMock.Setup(u => u.IsInRoleAsync(user, "Administrator")).ReturnsAsync(true);

			var listingId = Guid.NewGuid();
			var listing = new CarListing { Id = listingId, IsDeleted = false };
			_repoMock.Setup(r => r.GetByIdAsync(listingId.ToString())).ReturnsAsync(listing);

			var result = await _service.SoftDeleteCarListingAsync(listingId.ToString(), "adminUser");
			result.Should().BeTrue();
			listing.IsDeleted.Should().BeTrue();
			_repoMock.Verify(r => r.UpdateAsync(listing), Times.Once);
		}

		[Test]
		public async Task GetDeleteCarListingDtoByIdAsync_UserNotFound_ReturnsNull()
		{
			_userManagerMock.Setup(u => u.FindByIdAsync("u1")).ReturnsAsync((CarBazaarUser)null);

			var result = await _service.GetDeleteCarListingDtoByIdAsync(Guid.NewGuid().ToString(), "u1");
			result.Should().BeNull();
		}

		[Test]
		public async Task GetDeleteCarListingDtoByIdAsync_NotAdminOrNotOwner_ReturnsNull()
		{
			var user = new CarBazaarUser { Id = "userXYZ" };
			_userManagerMock.Setup(u => u.FindByIdAsync("userXYZ")).ReturnsAsync(user);
			_userManagerMock.Setup(u => u.IsInRoleAsync(user, "Administrator")).ReturnsAsync(false);

			_userCarListingServiceMock.Setup(u => u.GetByCarIdAsync(It.IsAny<string>())).ReturnsAsync((UserCarListing)null);

			var result = await _service.GetDeleteCarListingDtoByIdAsync(Guid.NewGuid().ToString(), "userXYZ");
			result.Should().BeNull();
		}

		[Test]
		public async Task GetDeleteCarListingDtoByIdAsync_Found_ReturnsDto()
		{
			var user = new CarBazaarUser { Id = "adminUser" };
			_userManagerMock.Setup(u => u.FindByIdAsync("adminUser")).ReturnsAsync(user);
			_userManagerMock.Setup(u => u.IsInRoleAsync(user, "Administrator")).ReturnsAsync(true);

			var listingId = Guid.NewGuid();
			var listing = new CarListing { Id = listingId, Name = "ToDelete" };
			_repoMock.Setup(r => r.GetByIdAsync(listingId.ToString())).ReturnsAsync(listing);

			var result = await _service.GetDeleteCarListingDtoByIdAsync(listingId.ToString(), "adminUser");
			result.Should().NotBeNull();
			result!.Name.Should().Be("ToDelete");
		}

		[Test]
		public async Task SearchCarListingsAsync_WithFilters_ReturnsEmptyList()
		{
			var allListings = new List<CarListing>
		{
			new CarListing { Id = Guid.NewGuid(), Name="CarA", Price=30000, Brand="BrandX", Type="Sedan"},
			new CarListing { Id = Guid.NewGuid(), Name="CarB", Price=80000, Brand="BrandY", Type="SUV"}
		};

			var filtered = allListings
			.Where(c => c.Type == "SUV" && c.Brand == "BrandA" && c.Price <= 10000)
			.ToList();

			var paginated = new PaginatedList<CarListing>(filtered, filtered.Count, 1, 10);

			_repoMock.Setup(r => r.GetBaseQuery()).Returns(allListings.AsQueryable());
			_repoMock.Setup(r => r.GetPaginatedAsync(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<IQueryable<CarListing>>()))
		    .ReturnsAsync(paginated);

			var dto = new CarListingSearchDto { Type = "SUV", Brand = "BrandA", PriceRange = "0-10000" };
			var result = await _service.SearchCarListingsAsync(dto, 1, 10);

			result.Items.Should().BeEmpty();
			result.TotalPages.Should().Be(0);
		}

		[Test]
		public async Task SearchCarListingsAsync_WithFilters_ReturnsFiltered()
		{
			var allListings = new List<CarListing>
		    {
			new CarListing { Id = Guid.NewGuid(), Name = "Car1", Price = 5000, Brand = "BrandA", Type = "SUV" },
			new CarListing { Id = Guid.NewGuid(), Name = "Car2", Price = 20000, Brand = "BrandB", Type = "Sedan" }
		    };

			var filtered = allListings
			.Where(c => c.Type == "SUV" && c.Brand == "BrandA" && c.Price <= 10000)
			.ToList();

			var paginated = new PaginatedList<CarListing>(filtered, filtered.Count, 1, 10);

			_repoMock.Setup(r => r.GetBaseQuery()).Returns(allListings.AsQueryable());
			_repoMock.Setup(r => r.GetPaginatedAsync(It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<IQueryable<CarListing>>()))
			.ReturnsAsync(paginated);

			var dto = new CarListingSearchDto { Type = "SUV", Brand = "BrandA", PriceRange = "0-10000" };
			var result = await _service.SearchCarListingsAsync(dto, 1, 10);

			result.Items.Should().HaveCount(1);
			result.Items[0].Name.Should().Be("Car1");
			result.TotalPages.Should().Be(1);
		}

		[Test]
		public async Task GetPaginatedCarListingsAsync_ReturnsPaginatedResults()
		{
			var allListings = new List<CarListing> { new CarListing { Id = Guid.NewGuid(), Name = "PageCar" } };
			var paginated = new PaginatedList<CarListing>(allListings, allListings.Count, 1, 10);

			_repoMock.Setup(r => r.GetPaginatedAsync(1, 10, null)).ReturnsAsync(paginated);

			var result = await _service.GetPaginatedCarListingsAsync(1, 10);

			result.Items.Should().HaveCount(1);
			result.Items[0].Name.Should().Be("PageCar");
			result.TotalPages.Should().Be(1);
		}
	}
}