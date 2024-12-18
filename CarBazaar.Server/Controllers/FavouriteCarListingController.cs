﻿using CarBazaar.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
    [Route("/api/[controller]")]
    [Authorize]
    public class FavouriteCarListingController(IFavouriteCarListingService favouriteCarListingService) : BaseController
    {
        [HttpGet("get-favourites")]
        public async Task<IActionResult> GetFavourites([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            string? userId = GetUserId();
            if (userId == null)
            {
                return BadRequest();
            }

            var favourites = await favouriteCarListingService.GetFavouritesAsync(userId, page, pageSize);
            return Ok(favourites);
        }

        [HttpPost("{carId}")]
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

        [HttpDelete("{carId}")]
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