using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Data.Models
{
	public class UserCarListing
	{
		[Comment("User Id")]
		public string UserId { get; set; } = null!;

		[ForeignKey("UserId")]
		[Comment("User")]
		public CarBazaarUser User { get; set; } = null!;

		[Comment("Car Listing Id")]
		public Guid CarListingId {  get; set; }

		[ForeignKey("CarListingId")]
		[Comment("Car Listing")]
		public CarListing CarListing { get; set; } = null!;
	}
}