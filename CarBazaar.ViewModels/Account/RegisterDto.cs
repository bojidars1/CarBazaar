using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Account
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Email is required!")]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[MinLength(6, ErrorMessage = "Password must be atleast 6 characters.")]
		public string Password { get; set; } = null!;

		[Required]
		[MinLength(6, ErrorMessage = "Password must be atleast 6 characters.")]
		public string ConfirmPassword { get; set; } = null!;
	}
}