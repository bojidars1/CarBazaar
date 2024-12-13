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
                return BadRequest("Adding failed");
            }

            return Ok("Added to favourites successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromFavourite(string carId)
        {
            string? userId = GetUserId();
            if (userId == null)
            {
                return BadRequest();
            }

            bool isDeleted = await favouriteCarListingService.DeleteFavouriteAsync(carId, userId);
            if(!isDeleted)
            {
                return BadRequest("Deletion failed");
            }

            return Ok("Removed from favourites successfully");
        }
    }
}