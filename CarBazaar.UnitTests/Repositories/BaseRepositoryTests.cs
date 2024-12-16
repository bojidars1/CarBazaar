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
	public class BaseRepositoryTests
	{
		[Test]
		public async Task AddAsync_ShouldAddEntity()
		{
			var context = DbContextHelper.GetInMemoryDbContext("AddTest");
			var repo = new Repository<CarListing>(context);

			var car = new CarListing
			{
				Id = Guid.NewGuid(),
				Name = "TestCar",
				Type = "Sedan",
				Brand = "Mercedes",
				Price = 1000,
				Gearbox = "Manual",
				State = "New",
				Km = 0,
				ProductionYear = 2017,
				Horsepower = 211,
				Color = "Black",
				ExtraInfo = "Best car ever",
				ImageURL = "some image",
				PublicationDate = DateTime.Now
			};
			await repo.AddAsync(car);

			var result = await repo.GetByIdAsync(car.Id.ToString());
			result.Should().NotBeNull();
			result!.Name.Should().Be("TestCar");
		}

		[Test]
		public async Task GetAllAsync_ReturnsAll()
		{
			var context = DbContextHelper.GetInMemoryDbContext("GetAllTest");
			var repo = new Repository<CarListing>(context);

			var car1 = new CarListing
			{
				Id = Guid.NewGuid(),
				Name = "Car 1",
				Type = "Sedan",
				Brand = "Mercedes",
				Price = 1000,
				Gearbox = "Manual",
				State = "New",
				Km = 0,
				ProductionYear = 2017,
				Horsepower = 211,
				Color = "Black",
				ExtraInfo = "Best car ever",
				ImageURL = "some image",
				PublicationDate = DateTime.Now
			};
			var car2 = new CarListing
			{
				Id = Guid.NewGuid(),
				Name = "Car 2",
				Type = "Sedan",
				Brand = "Mercedes",
				Price = 1000,
				Gearbox = "Manual",
				State = "New",
				Km = 0,
				ProductionYear = 2017,
				Horsepower = 211,
				Color = "Black",
				ExtraInfo = "Best car ever",
				ImageURL = "some image",
				PublicationDate = DateTime.Now
			};

			await repo.AddAsync(car1);
			await repo.AddAsync(car2);

			var all = await repo.GetAllAsync();
			all.Should().HaveCount(2);
		}

		[Test]
		public async Task UpdateAsync_UpdatesEntity()
		{
			var context = DbContextHelper.GetInMemoryDbContext("UpdateTest");
			var repo = new Repository<CarListing>(context);

			var car = new CarListing
			{
				Id = Guid.NewGuid(),
				Name = "OldName",
				Type = "Sedan",
				Brand = "Mercedes",
				Price = 1000,
				Gearbox = "Manual",
				State = "New",
				Km = 0,
				ProductionYear = 2017,
				Horsepower = 211,
				Color = "Black",
				ExtraInfo = "Best car ever",
				ImageURL = "some image",
				PublicationDate = DateTime.Now
			};
			await repo.AddAsync(car);

			car.Name = "NewName";
			await repo.UpdateAsync(car);

			var result = await repo.GetByIdAsync(car.Id.ToString());
			result!.Name.Should().Be("NewName");
		}

		[Test]
		public async Task DeleteByIdAsync_RemovesEntity()
		{
			var context = DbContextHelper.GetInMemoryDbContext("DeleteByIdTest");
			var repo = new Repository<CarListing>(context);

			var car = new CarListing
			{
				Id = Guid.NewGuid(),
				Name = "OldName",
				Type = "Sedan",
				Brand = "Mercedes",
				Price = 1000,
				Gearbox = "Manual",
				State = "New",
				Km = 0,
				ProductionYear = 2017,
				Horsepower = 211,
				Color = "Black",
				ExtraInfo = "Best car ever",
				ImageURL = "some image",
				PublicationDate = DateTime.Now
			};
			await repo.AddAsync(car);

			await repo.DeleteByIdAsync(car.Id.ToString());
			var result = await repo.GetByIdAsync(car.Id.ToString());
			result.Should().BeNull();
		}
	}
}