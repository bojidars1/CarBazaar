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
	public class NotificationServiceTests
	{
		private Mock<INotificationRepository> _notificationRepoMock;
		private NotificationService _service;

		[SetUp]
		public void Setup()
		{
			_notificationRepoMock = new Mock<INotificationRepository>();
			_service = new NotificationService(_notificationRepoMock.Object);
		}

		[Test]
		public async Task AddNotificationAsync_CallsAddOnRepository()
		{
			var notification = new Notification
			{
				Id = Guid.NewGuid(),
				UserId = "User1",
				Message = "Test message"
			};

			await _service.AddNotificationAsync(notification);

			_notificationRepoMock.Verify(r => r.AddAsync(notification), Times.Once);
		}

		[Test]
		public async Task DeleteAsync_NotificationNotFound_ReturnsFalse()
		{
			_notificationRepoMock.Setup(r => r.GetByIdAsync("notfId"))
				.ReturnsAsync((Notification)null);

			var result = await _service.DeleteAsync("UserX", "notfId");
			result.Should().BeFalse();
		}

		[Test]
		public async Task DeleteAsync_UserMismatch_ReturnsFalse()
		{
			var notf = new Notification { Id = Guid.NewGuid(), UserId = "AnotherUser" };
			_notificationRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(notf);

			var result = await _service.DeleteAsync("UserX", notf.Id.ToString());
			result.Should().BeFalse();
		}

		[Test]
		public async Task DeleteAsync_DeletesAndReturnsTrue()
		{
			var userId = "UserZ";
			var notf = new Notification { Id = Guid.NewGuid(), UserId = userId };
			_notificationRepoMock.Setup(r => r.GetByIdAsync(notf.Id.ToString())).ReturnsAsync(notf);

			var result = await _service.DeleteAsync(userId, notf.Id.ToString());
			result.Should().BeTrue();
			_notificationRepoMock.Verify(r => r.DeleteAsync(notf), Times.Once);
		}

		[Test]
		public async Task GetNotificationsAsync_ReturnsPaginatedDto()
		{
			var userId = "User123";
			var n1 = new Notification { Id = Guid.NewGuid(), UserId = userId, Message = "Msg1", IsRead = false };
			var n2 = new Notification { Id = Guid.NewGuid(), UserId = userId, Message = "Msg2", IsRead = true };
			var all = new List<Notification> { n1, n2 };

			var paginated = new PaginatedList<Notification>(all, all.Count, 1, 10);

			_notificationRepoMock.Setup(r => r.GetBaseQuery()).Returns(all.AsQueryable());
			_notificationRepoMock.Setup(r => r.GetPaginatedAsync(1, 10, It.IsAny<IQueryable<Notification>>()))
				.ReturnsAsync(paginated);

			var result = await _service.GetNotificationsAsync(userId, 1, 10);
			result.Should().NotBeNull();
			result!.Items.Should().HaveCount(2);
			result.Items[0].Message.Should().Be("Msg1");
			result.Items[1].IsRead.Should().BeTrue();
			result.TotalPages.Should().Be(1);
		}

		[Test]
		public async Task MarkNotificationsAsReadAsync_CallsRepositoryMethod()
		{
			var ids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
			await _service.MarkNotificationsAsReadAsync(ids);

			_notificationRepoMock.Verify(r => r.MarkAsReadAsync(ids), Times.Once);
		}
	}
}