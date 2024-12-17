using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Contracts
{
    public interface IJwtService
    {
        public Task<string> GenerateAccessToken(string userId, string email);
    }
}