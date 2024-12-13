using CarBazaar.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
    [Route("/api/[controller]")]
    [Authorize]
    public class FavoriteCarListingController(IFavouriteCarListingService favouriteCarListingService) : BaseController
    {
        [HttpGet("get-favourites")]
        public async Task<IActionResult> GetFavourites()
        {
            string? userId = GetUserId();
            if (userId == null)
            {
                return BadRequest();
            }

            var favourites = await favouriteCarListingService.GetFavouritesAsync(userId);
            return Ok(favourites);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavourite(string carId)
        {
            string? userId = GetUserId();
            if (userId == null)
            {
                return BadRequest();
            }

            bool isAdded = await favouriteCarListingService.AddToFavouriteAsync(carId, userId);
            if (!isAdded)
            {
                return BadRequest();
            }

            return Ok("Added to favourites successfully");
        }
    }
}