using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Data
{
	public class CarBazaarDbContextFactory : IDesignTimeDbContextFactory<CarBazaarDbContext>
	{
		public CarBazaarDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<CarBazaarDbContext>();
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarBazaarDbContext;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new CarBazaarDbContext(optionsBuilder.Options);
		}
	}
}