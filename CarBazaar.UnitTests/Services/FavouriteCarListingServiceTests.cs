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
	public class FavouriteCarListingServiceTests
	{
		private Mock<IFavouriteCarListingRepository> _favouriteRepoMock;
		private Mock<ICarListingRepository> _carListingRepoMock;
		private FavouriteCarListingService _service;

		[SetUp]
		public void Setup()
		{
			_favouriteRepoMock = new Mock<IFavouriteCarListingRepository>();
			_carListingRepoMock = new Mock<ICarListingRepository>();

			_service = new FavouriteCarListingService(_favouriteRepoMock.Object, _carListingRepoMock.Object);
		}

		[Test]
		public async Task AddToFavouriteAsync_NoCarFound_ReturnsFalse()
		{
			_carListingRepoMock.Setup(r => r.GetByIdAsync("car1")).ReturnsAsync((CarListing)null);

			var result = await _service.AddToFavouriteAsync("car1", "userX");
			result.Should().BeFalse();
		}

		[Test]
		public async Task AddToFavouriteAsync_AlreadyFavourite_ReturnsFalse()
		{
			var carId = Guid.NewGuid();
			_carListingRepoMock.Setup(r => r.GetByIdAsync(carId.ToString())).ReturnsAsync(new CarListing { Id = carId });
			_favouriteRepoMock.Setup(r => r.GetByUserAndCarIdAsync(carId.ToString(), "userX")).ReturnsAsync(new FavouriteCarListing());

			var result = await _service.AddToFavouriteAsync(carId.ToString(), "userX");
			result.Should().BeFalse();
		}

		[Test]
		public async Task AddToFavouriteAsync_AddsAndReturnsTrue()
		{
			var carId = Guid.NewGuid();
			_carListingRepoMock.Setup(r => r.GetByIdAsync(carId.ToString())).ReturnsAsync(new CarListing { Id = carId });
			_favouriteRepoMock.Setup(r => r.GetByUserAndCarIdAsync(carId.ToString(), "userX")).ReturnsAsync((FavouriteCarListing)null);

			var result = await _service.AddToFavouriteAsync(carId.ToString(), "userX");
			result.Should().BeTrue();
			_favouriteRepoMock.Verify(r => r.AddAsync(It.Is<FavouriteCarListing>(f => f.UserId == "userX" && f.CarListingId == carId)), Times.Once);
		}

		[Test]
		public async Task DeleteFavouriteAsync_NoCarFound_ReturnsFalse()
		{
			_carListingRepoMock.Setup(r => r.GetByIdAsync("carX")).ReturnsAsync((CarListing)null);

			var result = await _service.DeleteFavouriteAsync("carX", "userY");
			result.Should().BeFalse();
		}

		[Test]
		public async Task DeleteFavouriteAsync_NotFavourite_ReturnsFalse()
		{
			var carId = Guid.NewGuid();
			_carListingRepoMock.Setup(r => r.GetByIdAsync(carId.ToString())).ReturnsAsync(new CarListing { Id = carId });
			_favouriteRepoMock.Setup(r => r.GetByUserAndCarIdAsync(carId.ToString(), "userY")).ReturnsAsync((FavouriteCarListing)null);

			var result = await _service.DeleteFavouriteAsync(carId.ToString(), "userY");
			result.Should().BeFalse();
		}

		[Test]
		public async Task DeleteFavouriteAsync_DeletesAndReturnsTrue()
		{
			var carId = Guid.NewGuid();
			var fav = new FavouriteCarListing { UserId = "userZ", CarListingId = carId };
			_carListingRepoMock.Setup(r => r.GetByIdAsync(carId.ToString())).ReturnsAsync(new CarListing { Id = carId });
			_favouriteRepoMock.Setup(r => r.GetByUserAndCarIdAsync(carId.ToString(), "userZ")).ReturnsAsync(fav);

			var result = await _service.DeleteFavouriteAsync(carId.ToString(), "userZ");
			result.Should().BeTrue();
			_favouriteRepoMock.Verify(r => r.DeleteAsync(fav), Times.Once);
		}

		[Test]
		public async Task GetFavouritesAsync_ReturnsPaginatedDto()
		{
			var userId = "userABC";
			var carId = Guid.NewGuid();
			var favList = new List<FavouriteCarListing>
		{
			new FavouriteCarListing
			{
				UserId = userId,
				CarListingId = carId,
				CarListing = new CarListing { Id = carId, Name = "FavCar", Price = 10000, ImageURL = "img.jpg" }
			}
		};

			var paginated = new PaginatedList<FavouriteCarListing>(favList, favList.Count, 1, 10);

			_favouriteRepoMock.Setup(r => r.GetBaseQuery()).Returns(favList.AsQueryable());
			_favouriteRepoMock.Setup(r => r.GetPaginatedAsync(1, 10, It.IsAny<IQueryable<FavouriteCarListing>>()))
				.ReturnsAsync(paginated);

			var result = await _service.GetFavouritesAsync(userId, 1, 10);
			result.Should().NotBeNull();
			result!.Items.Should().HaveCount(1);
			result.Items[0].Name.Should().Be("FavCar");
			result.TotalPages.Should().Be(1);
		}

		[Test]
		public async Task GetFavouritesAsync_NoFavourites_ReturnsEmptyDto()
		{
			var userId = "userNoFav";
			var emptyList = new List<FavouriteCarListing>();
			var paginated = new PaginatedList<FavouriteCarListing>(emptyList, 0, 1, 10);

			_favouriteRepoMock.Setup(r => r.GetBaseQuery()).Returns(emptyList.AsQueryable());
			_favouriteRepoMock.Setup(r => r.GetPaginatedAsync(1, 10, It.IsAny<IQueryable<FavouriteCarListing>>()))
				.ReturnsAsync(paginated);

			var result = await _service.GetFavouritesAsync(userId, 1, 10);

			result.Should().NotBeNull();
			result!.Items.Should().BeEmpty();
			result.TotalPages.Should().Be(0);
		}
	}
}