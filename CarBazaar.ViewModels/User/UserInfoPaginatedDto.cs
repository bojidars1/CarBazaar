using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.User
{
	public class UserInfoPaginatedDto
	{
		public List<UserInfoDto> Items { get; set; } = new List<UserInfoDto>();

		public int TotalPages { get; set; }
	}
}