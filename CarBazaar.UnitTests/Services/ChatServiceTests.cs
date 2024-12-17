using CarBazaar.Data.Models;
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
	public class ChatServiceTests
	{
		private Mock<IUserCarListingRepository> _userCarListingRepoMock;
		private Mock<IChatRepository> _chatRepoMock;
		private Mock<IUserRepository> _userRepoMock;
		private ChatService _service;

		[SetUp]
		public void Setup()
		{
			_userCarListingRepoMock = new Mock<IUserCarListingRepository>();
			_chatRepoMock = new Mock<IChatRepository>();
			_userRepoMock = new Mock<IUserRepository>();
			_service = new ChatService(_userCarListingRepoMock.Object, _chatRepoMock.Object, _userRepoMock.Object);
		}

		[Test]
		public async Task GetChatSummariesAsync_NoChatMessages_ReturnsEmptyPaginatedDto()
		{
			string userId = "user123";
			_chatRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<ChatMessage>());
			_userRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<CarBazaarUser>());

			var result = await _service.GetChatSummariesAsync(userId, 1, 10);

			result.Should().NotBeNull();
			result.Items.Should().BeEmpty();
			result.TotalPages.Should().Be(0);
		}

		[Test]
		public async Task GetChatSummariesAsync_NoChatsForUser_ReturnsEmptyPaginatedDto()
		{
			string userId = "user123";
			var chatMessages = new List<ChatMessage>
			{
				new ChatMessage { CarListingId = Guid.NewGuid(), SenderId = "userA", ReceiverId = "userB", Message = "Hello", Timestamp = DateTime.UtcNow }
			};
			_chatRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(chatMessages);
			_userRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<CarBazaarUser>());

			var result = await _service.GetChatSummariesAsync(userId, 1, 10);

			result.Should().NotBeNull();
			result.Items.Should().BeEmpty();
			result.TotalPages.Should().Be(0);
		}

		[Test]
		public async Task GetChatSummariesAsync_WithChats_ReturnsCorrectPaginatedDto()
		{
			string userId = "user123";
			var carListingId = Guid.NewGuid();
			var chatMessages = new List<ChatMessage>
			{
				new ChatMessage { CarListingId = carListingId, SenderId = "user123", ReceiverId = "userA", Message = "Hi A", Timestamp = DateTime.UtcNow.AddMinutes(-10) },
				new ChatMessage { CarListingId = carListingId, SenderId = "userA", ReceiverId = "user123", Message = "Hello User123", Timestamp = DateTime.UtcNow.AddMinutes(-5) }
			};
			var users = new List<CarBazaarUser>
			{
				new CarBazaarUser { Id = "userA", Email = "a@example.com" }
			};

			_chatRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(chatMessages);
			_userRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

			var result = await _service.GetChatSummariesAsync(userId, 1, 10);

			result.Should().NotBeNull();
			result.Items.Should().HaveCount(1);
			var summary = result.Items.First();
			summary.CarListingId.Should().Be(carListingId);
			summary.OtherParticipantId.Should().Be("userA");
			summary.OtherParticipantName.Should().Be("a@example.com");
			summary.LastMessage.Should().Be("Hello User123");
			summary.LastMessageTimestamp.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(-5), TimeSpan.FromSeconds(1));
			result.TotalPages.Should().Be(1);
		}

		[Test]
		public async Task GetChatSummariesAsync_Pagination_WorksCorrectly()
		{
			string userId = "user123";
			var carListingId1 = Guid.NewGuid();
			var carListingId2 = Guid.NewGuid();
			var chatMessages = new List<ChatMessage>
			{
				new ChatMessage { CarListingId = carListingId1, SenderId = "user123", ReceiverId = "userA", Message = "Hi A1", Timestamp = DateTime.UtcNow.AddMinutes(-30) },
				new ChatMessage { CarListingId = carListingId1, SenderId = "userA", ReceiverId = "user123", Message = "Hello A1", Timestamp = DateTime.UtcNow.AddMinutes(-25) },
				new ChatMessage { CarListingId = carListingId2, SenderId = "user123", ReceiverId = "userB", Message = "Hi B1", Timestamp = DateTime.UtcNow.AddMinutes(-20) },
				new ChatMessage { CarListingId = carListingId2, SenderId = "userB", ReceiverId = "user123", Message = "Hello B1", Timestamp = DateTime.UtcNow.AddMinutes(-15) }
			};
			var users = new List<CarBazaarUser>
			{
				new CarBazaarUser { Id = "userA", Email = "a@example.com" },
				new CarBazaarUser { Id = "userB", Email = "b@example.com" }
			};

			_chatRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(chatMessages);
			_userRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

			var resultPage1 = await _service.GetChatSummariesAsync(userId, 1, 1);

			var resultPage2 = await _service.GetChatSummariesAsync(userId, 2, 1);

			resultPage1.Should().NotBeNull();
			resultPage1.Items.Should().HaveCount(2);
			resultPage1.TotalPages.Should().Be(2);

			resultPage2.Should().NotBeNull();
			resultPage2.Items.Should().HaveCount(2);
			resultPage2.TotalPages.Should().Be(2);
		}

		[Test]
		public async Task GetMessagesAsync_NoMessages_ReturnsEmptyList()
		{
			string carListingId = Guid.NewGuid().ToString();
			string userId = "user123";
			string participantId = "userA";

			_chatRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<ChatMessage>());

			var result = await _service.GetMessagesAsync(carListingId, userId, participantId);

			result.Should().BeEmpty();
		}

		[Test]
		public async Task GetMessagesAsync_WithMessages_ReturnsOrderedMessages()
		{
			string carListingId = Guid.NewGuid().ToString();
			string userId = "user123";
			string participantId = "userA";

			var chatMessages = new List<ChatMessage>
			{
				new ChatMessage { CarListingId = Guid.Parse(carListingId), SenderId = "user123", ReceiverId = "userA", Message = "Hello", Timestamp = DateTime.UtcNow.AddMinutes(-10) },
				new ChatMessage { CarListingId = Guid.Parse(carListingId), SenderId = "userA", ReceiverId = "user123", Message = "Hi there!", Timestamp = DateTime.UtcNow.AddMinutes(-5) }
			};

			_chatRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(chatMessages);

			var result = await _service.GetMessagesAsync(carListingId, userId, participantId);

			result.Should().HaveCount(2);
			result[0].Message.Should().Be("Hello");
			result[0].SenderId.Should().Be("user123");
			result[0].ParticipantId.Should().Be("userA");

			result[1].Message.Should().Be("Hi there!");
			result[1].SenderId.Should().Be("userA");
			result[1].ParticipantId.Should().Be("user123");
		}

		[Test]
		public async Task IsOneOfThemOwner_UserIsOwner_ReturnsTrue()
		{
			string userId = "user123";
			string receiverId = "userA";
			string carListingId = Guid.NewGuid().ToString();

			var userCarListings = new List<UserCarListing>
			{
				new UserCarListing { UserId = userId, CarListingId = Guid.Parse(carListingId) }
			};

			_userCarListingRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(userCarListings);

			var result = await _service.IsOneOfThemOwner(userId, receiverId, carListingId);

			result.Should().BeTrue();
		}

		[Test]
		public async Task IsOneOfThemOwner_ReceiverIsOwner_ReturnsTrue()
		{
			string userId = "user123";
			string receiverId = "userA";
			string carListingId = Guid.NewGuid().ToString();

			var userCarListings = new List<UserCarListing>
			{
				new UserCarListing { UserId = receiverId, CarListingId = Guid.Parse(carListingId) }
			};

			_userCarListingRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(userCarListings);

			var result = await _service.IsOneOfThemOwner(userId, receiverId, carListingId);

			result.Should().BeTrue();
		}

		[Test]
		public async Task IsOneOfThemOwner_NeitherIsOwner_ReturnsFalse()
		{
			string userId = "user123";
			string receiverId = "userA";
			string carListingId = Guid.NewGuid().ToString();

			var userCarListings = new List<UserCarListing>
			{
				new UserCarListing { UserId = "userB", CarListingId = Guid.Parse(carListingId) }
			};

			_userCarListingRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(userCarListings);

			var result = await _service.IsOneOfThemOwner(userId, receiverId, carListingId);

			result.Should().BeFalse();
		}

		[Test]
		public async Task SendMessageAsync_ValidInput_AddsMessage()
		{
			string userId = "user123";
			string receiverId = "userA";
			Guid carListingId = Guid.NewGuid();
			string message = "Hello!";

			_chatRepoMock.Setup(r => r.AddAsync(It.IsAny<ChatMessage>())).Returns(Task.CompletedTask);

			await _service.SendMessageAsync(userId, receiverId, carListingId, message);

			_chatRepoMock.Verify(r => r.AddAsync(It.Is<ChatMessage>(cm =>
				cm.SenderId == userId &&
				cm.ReceiverId == receiverId &&
				cm.CarListingId == carListingId &&
				cm.Message == message &&
				cm.Timestamp <= DateTime.UtcNow
			)), Times.Once);
		}

		[Test]
		public void SendMessageAsync_ErrorAddingMessage_ThrowsException()
		{
			string userId = "user123";
			string receiverId = "userA";
			Guid carListingId = Guid.NewGuid();
			string message = "Hello!";

			_chatRepoMock.Setup(r => r.AddAsync(It.IsAny<ChatMessage>())).ThrowsAsync(new Exception("DB Error"));

			Func<Task> act = async () => await _service.SendMessageAsync(userId, receiverId, carListingId, message);

			act.Should().ThrowAsync<Exception>().WithMessage("DB Error");
		}
	}
}