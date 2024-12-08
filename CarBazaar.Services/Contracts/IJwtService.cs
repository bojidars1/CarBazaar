﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Contracts
{
    public interface IJwtService
    {
        public string GenerateAccessToken(string userId, string email);
        public string GenerateRefreshToken();
    }
}