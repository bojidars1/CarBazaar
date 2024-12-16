using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories;
using CarBazaar.UnitTests.Common;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.UnitTests.Repositories
{
	[TestFixture]
	public class UserCarListingRepositoryTests
	{
		[Test]
		public async Task GetByCarIdAsync_ReturnsListing()
		{
			var context = DbContextHelper.GetInMemoryDbContext("UCLCarTest");
			var repo = new UserCarListingRepository(context);

			var carId = Guid.NewGuid();
			await repo.AddAsync(new UserCarListing { UserId = "UserA", CarListingId = carId });

			var result = await repo.GetByCarIdAsync(carId.ToString());
			result.Should().NotBeNull();
			result!.CarListingId.Should().Be(carId);
		}

		[Test]
		public async Task GetByCarIdAsync_NotFound_ReturnsNull()
		{
			var context = DbContextHelper.GetInMemoryDbContext("UCLCarNotFoundTest");
			var repo = new UserCarListingRepository(context);

			var result = await repo.GetByCarIdAsync(Guid.NewGuid().ToString());
			result.Should().BeNull();
		}

		[Test]
		public async Task GetByUserIdAsync_ReturnsListing()
		{
			var context = DbContextHelper.GetInMemoryDbContext("UCLUserTest");
			var repo = new UserCarListingRepository(context);

			var userId = "UserB";
			await repo.AddAsync(new UserCarListing { UserId = userId, CarListingId = Guid.NewGuid() });

			var result = await repo.GetByUserIdAsync(userId);
			result.Should().NotBeNull();
			result!.UserId.Should().Be(userId);
		}

		[Test]
		public async Task GetByUserIdAsync_NotFound_ReturnsNull()
		{
			var context = DbContextHelper.GetInMemoryDbContext("UCLUserNotFoundTest");
			var repo = new UserCarListingRepository(context);

			var result = await repo.GetByUserIdAsync("NonUser");
			result.Should().BeNull();
		}
	}
}