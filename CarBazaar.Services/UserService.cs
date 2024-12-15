using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class UserService(IUserRepository userRepository) : IUserService
	{
		public async Task<UserInfoPaginatedDto> GetUserInfoPaginated(int page, int pageSize)
		{
			var paginatedUsers = await userRepository.GetPaginatedAsync(page, pageSize);
			var totalPages = paginatedUsers.TotalPages;

			var items = paginatedUsers.Select(u => new UserInfoDto
			{
				Id = u.Id,
				Email = u.Email
			}).ToList();

			return new UserInfoPaginatedDto
			{
				Items = items,
				TotalPages = totalPages
			};
		}
	}
}