using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Data
{
    public class CarBazaarDbContext : DbContext
    {
        public CarBazaarDbContext(DbContextOptions<CarBazaarDbContext> options) : base(options) { }
    }
}