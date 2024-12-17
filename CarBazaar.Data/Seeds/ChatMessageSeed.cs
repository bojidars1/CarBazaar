using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;

namespace CarBazaar.Data.Seeds
{
	public class ChatMessageSeed : IEntityTypeConfiguration<ChatMessage>
	{
		public void Configure(EntityTypeBuilder<ChatMessage> builder)
		{
			builder.HasData(new ChatMessage
			{
				Id = Guid.NewGuid(),
				SenderId = "user-1",
				ReceiverId = "user-2",
				CarListingId = carListing1.Id,
				Message = "Is the car still available?",
				Timestamp = DateTime.UtcNow
			});
		}
	}
}