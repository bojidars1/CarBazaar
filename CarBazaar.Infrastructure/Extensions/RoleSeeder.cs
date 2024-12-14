using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Extensions
{
	public class RoleSeeder
	{
		public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			var roles = new[] { "User", "Administrator" };

			foreach (var role in roles)
			{
				var roleExists = await roleManager.RoleExistsAsync(role);
				if (!roleExists)
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}
	}
}