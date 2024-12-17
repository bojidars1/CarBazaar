using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CarBazaar.Data.Configurations
{
	public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
	{
		public void Configure(EntityTypeBuilder<ChatMessage> builder)
		{
			builder.HasKey(cm => cm.Id);

			builder.HasOne(cm => cm.Sender)
				.WithMany()
				.HasForeignKey(cm => cm.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(cm => cm.Receiver)
				.WithMany()
				.HasForeignKey(cm => cm.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(cm => cm.CarListing)
				.WithMany()
				.HasForeignKey(cm => cm.CarListingId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}