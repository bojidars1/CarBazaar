using CarBazaar.Data;
using Microsoft.EntityFrameworkCore;

namespace CarBazaar.UnitTests.Common
{
	public static class DbContextHelper
	{
		public static CarBazaarDbContext GetInMemoryDbContext(string dbName)
		{
			var options = new DbContextOptionsBuilder<CarBazaarDbContext>()
				.UseInMemoryDatabase(dbName)
				.Options;

			return new CarBazaarDbContext(options);
		}
	}
}