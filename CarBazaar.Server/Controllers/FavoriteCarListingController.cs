using CarBazaar.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
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
    }
}