using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Extensions;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
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
	public class UserServiceTests
	{
		private Mock<IUserRepository> _userRepoMock;
		private Mock<UserManager<CarBazaarUser>> _userManagerMock;
		private UserService _service;

		[SetUp]
		public void Setup()
		{
			_userRepoMock = new Mock<IUserRepository>();

			var store = new Mock<IUserStore<CarBazaarUser>>();
			_userManagerMock = new Mock<UserManager<CarBazaarUser>>(
				store.Object, null, null, null, null, null, null, null, null
			);

			_service = new UserService(_userRepoMock.Object, _userManagerMock.Object);
		}

		[Test]
		public async Task GetUserInfoPaginated_NoUsers_ReturnsEmptyDto()
		{
			var emptyList = new List<CarBazaarUser>();
			var paginated = new PaginatedList<CarBazaarUser>(emptyList, 0, 1, 10);

			_userRepoMock.Setup(r => r.GetPaginatedAsync(1, 10, null))
				.ReturnsAsync(paginated);

			var result = await _service.GetUserInfoPaginated(1, 10);
			result.Should().NotBeNull();
			result.Items.Should().BeEmpty();
			result.TotalPages.Should().Be(0);
		}

		[Test]
		public async Task GetUserInfoPaginated_ReturnsUsers()
		{
			var userId1 = "User1";
			var userId2 = "User2";
			var users = new List<CarBazaarUser>
		{
			new CarBazaarUser { Id = userId1, Email = "user1@test.com" },
			new CarBazaarUser { Id = userId2, Email = "user2@test.com" }
		};

			var paginated = new PaginatedList<CarBazaarUser>(users, users.Count, 1, 10);
			_userRepoMock.Setup(r => r.GetPaginatedAsync(1, 10, null)).ReturnsAsync(paginated);

			var result = await _service.GetUserInfoPaginated(1, 10);

			result.Should().NotBeNull();
			result.Items.Should().HaveCount(2);
			result.Items[0].Id.Should().Be(userId1);
			result.Items[0].Email.Should().Be("user1@test.com");
			result.Items[1].Id.Should().Be(userId2);
			result.Items[1].Email.Should().Be("user2@test.com");
			result.TotalPages.Should().Be(1);
		}

		[Test]
		public async Task GetUserInfoPaginated_MultiplePages_ReturnsCorrectPage()
		{
			var allUsers = new List<CarBazaarUser>
		{
			new CarBazaarUser { Id = "UserA", Email="userA@test.com" },
			new CarBazaarUser { Id = "UserB", Email="userB@test.com" },
			new CarBazaarUser { Id = "UserC", Email="userC@test.com" },
		};

			var page2Items = allUsers.Skip(1).Take(1).ToList();
			var paginated = new PaginatedList<CarBazaarUser>(page2Items, allUsers.Count, 2, 1);

			_userRepoMock.Setup(r => r.GetPaginatedAsync(2, 1, null)).ReturnsAsync(paginated);

			var result = await _service.GetUserInfoPaginated(2, 1);

			result.Items.Should().HaveCount(1);
			result.Items[0].Email.Should().Be("userB@test.com");
			result.TotalPages.Should().Be((int)Math.Ceiling(allUsers.Count / (double)1));
		}
	}
}