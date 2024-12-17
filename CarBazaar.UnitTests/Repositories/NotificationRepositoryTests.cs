using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories;
using CarBazaar.UnitTests.Common;
using FluentAssertions;
using NUnit.Framework;

namespace CarBazaar.UnitTests.Repositories
{
	[TestFixture]
	public class NotificationRepositoryTests
	{
		[Test]
		public async Task MarkAsReadAsync_ShouldSetIsReadToTrueForSpecificNotifications()
		{
			var context = DbContextHelper.GetInMemoryDbContext("NotifMarkAsReadTest");
			var repo = new NotificationRepository(context);

			var userId = "User1";
			var senderId = "User2";
			var carId = Guid.NewGuid();

			var n1 = new Notification { Id = Guid.NewGuid(), UserId = userId, SenderId = senderId, CarListingId = carId, Message = "Msg1", IsRead = false };
			var n2 = new Notification { Id = Guid.NewGuid(), UserId = userId, SenderId = senderId, CarListingId = carId, Message = "Msg2", IsRead = false };
			var n3 = new Notification { Id = Guid.NewGuid(), UserId = userId, SenderId = senderId, CarListingId = carId, Message = "Msg3", IsRead = false };

			await repo.AddAsync(n1);
			await repo.AddAsync(n2);
			await repo.AddAsync(n3);

			await repo.MarkAsReadAsync(new List<Guid> { n1.Id, n3.Id });

			var updated1 = await repo.GetByIdAsync(n1.Id.ToString());
			var updated2 = await repo.GetByIdAsync(n2.Id.ToString());
			var updated3 = await repo.GetByIdAsync(n3.Id.ToString());

			updated1!.IsRead.Should().BeTrue("n1 was included in the read list");
			updated2!.IsRead.Should().BeFalse("n2 was not included in the read list");
			updated3!.IsRead.Should().BeTrue("n3 was included in the read list");
		}

		[Test]
		public async Task MarkAsReadAsync_ShouldIgnoreNonExistentNotifications()
		{
			var context = DbContextHelper.GetInMemoryDbContext("NotifIgnoreNonExistentTest");
			var repo = new NotificationRepository(context);

			var userId = "UserX";
			var senderId = "UserY";
			var carId = Guid.NewGuid();

			var n1 = new Notification { Id = Guid.NewGuid(), UserId = userId, SenderId = senderId, CarListingId = carId, Message = "ExistingMsg", IsRead = false };
			await repo.AddAsync(n1);

			await repo.MarkAsReadAsync(new List<Guid> { Guid.NewGuid() });

			var updated = await repo.GetByIdAsync(n1.Id.ToString());
			updated!.IsRead.Should().BeFalse("the existing notification should remain unread");
		}

		[Test]
		public async Task MarkAsReadAsync_NoIdsProvided_NoChangeOccurs()
		{
			var context = DbContextHelper.GetInMemoryDbContext("NotifNoIdsTest");
			var repo = new NotificationRepository(context);

			var userId = "UserA";
			var senderId = "UserB";
			var carId = Guid.NewGuid();

			var n1 = new Notification { Id = Guid.NewGuid(), UserId = userId, SenderId = senderId, CarListingId = carId, Message = "Msg", IsRead = false };
			await repo.AddAsync(n1);

			await repo.MarkAsReadAsync(new List<Guid>());

			var updated = await repo.GetByIdAsync(n1.Id.ToString());
			updated!.IsRead.Should().BeFalse("no IDs were provided, so no change should occur");
		}
	}
}