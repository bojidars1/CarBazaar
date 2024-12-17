using Microsoft.AspNetCore.Identity;

namespace CarBazaar.Data.Models
{
    public class CarBazaarUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}