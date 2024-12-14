﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Contracts
{
	public interface IChatService
	{
		Task SendMessageAsync(string userId, string receiverId, Guid carListingId, string message);

		Task<bool> IsOneOfThemOwner(string userId, string receiverId, string carListingId);
	}
}