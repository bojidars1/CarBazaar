﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.CarListing
{
    public class CarListingPaginatedSearchDto
    {
        public List<CarListingListDetailsDto> Items { get; set; } = new List<CarListingListDetailsDto>();

        public int TotalPages { get; set; }
    }
}