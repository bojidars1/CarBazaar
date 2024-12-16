using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories;
using CarBazaar.UnitTests.Common;
using FluentAssertions;
using NUnit.Framework;

namespace CarBazaar.UnitTests.Repositories
{
	[TestFixture]
	public class FavouriteCarListingRepositoryTests
	{
		[Test]
		public async Task GetByUserAndCarIdAsync_ReturnsCorrectFavourite()
		{
			var context = DbContextHelper.GetInMemoryDbContext("FavTest");
			var repo = new FavouriteCarListingRepository(context);

			var userId = "User123";
			var carId = Guid.NewGuid();
			await repo.AddAsync(new FavouriteCarListing { UserId = userId, CarListingId = carId });

			var result = await repo.GetByUserAndCarIdAsync(carId.ToString(), userId);

			result.Should().NotBeNull();
			result!.UserId.Should().Be(userId);
			result.CarListingId.Should().Be(carId);
		}

		[Test]
		public async Task GetByUserAndCarIdAsync_NotFound_ReturnsNull()
		{
			var context = DbContextHelper.GetInMemoryDbContext("FavNotFoundTest");
			var repo = new FavouriteCarListingRepository(context);

			var result = await repo.GetByUserAndCarIdAsync(Guid.NewGuid().ToString(), "nonUser");
			result.Should().BeNull();
		}
	}
}