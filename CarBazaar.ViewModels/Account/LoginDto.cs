using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Account
{
	public class LoginDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[MinLength(6, ErrorMessage = "Password must be atleast 6 characters.")]
		public string Password { get; set; } = null!;
	}
}