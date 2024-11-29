using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.CarListing
{
	public class AddCarListingDto
	{
		[Required(ErrorMessage = "Name is required.")]
		[StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "Type is required.")]
		[StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
		public string Type { get; set; } = null!;

		[Required(ErrorMessage = "Brand is required.")]
		[StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters.")]
		public string Brand { get; set; } = null!;

		[Required(ErrorMessage = "Price is required.")]
		[Range(1, 100000000, ErrorMessage = "Price must be between 1 and 100,000,000.")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Gearbox is required.")]
		[StringLength(20, ErrorMessage = "Gearbox cannot exceed 20 characters")]
		public string Gearbox { get; set; } = null!;

		[Required(ErrorMessage = "State is required.")]
		[StringLength(20, ErrorMessage = "State cannot exceed 20 characters")]
		public string State { get; set; } = null!;

		[Required(ErrorMessage = "Kilometers driven is required.")]
		[Range(0, 1000000, ErrorMessage = "Kilometers must be between 0 and 1,000,000.")]
		public long Km { get; set; }

		[Required(ErrorMessage = "Production Year is required.")]
		[Range(1900, 2024, ErrorMessage = "Production Year must be between 1900 and 2024.")]
		public int ProductionYear { get; set; }

		[Required(ErrorMessage = "Horsepower is required.")]
		[Range(1, 2000, ErrorMessage = "Horsepower must be between 1 and 2000.")]
		public int Horsepower { get; set; }

		[Required(ErrorMessage = "Color is required.")]
		[StringLength(30, ErrorMessage = "Color cannot exceed 30 characters.")]
		public string Color { get; set; } = null!;

		[StringLength(500, ErrorMessage = "Info cannot exceed 500 characters.")]
		public string ExtraInfo { get; set; } = string.Empty;

		[StringLength(10000, ErrorMessage = "Image URL cannot exceed 10K characters.")]
		public string ImageURL { get; set; } = string.Empty;
	}
}