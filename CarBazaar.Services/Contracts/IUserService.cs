using CarBazaar.Data.Models;
using CarBazaar.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Contracts
{
	public interface IUserService
	{
		Task<UserInfoPaginatedDto> GetUserInfoPaginated(int page, int pageSize);

		Task<CarBazaarUser?> GetUserByUserIdAsync(string userId);
	}
}