using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Notifcations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("/api/[controller]")]
	[Authorize]
	public class NotificationController(INotificationService notificationService) : BaseController
	{
		[HttpGet("get-notifications")]
		public async Task<IActionResult> GetNotifications([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			string? userId = GetUserId();

			if (string.IsNullOrEmpty(userId))
			{
				return BadRequest();
			}

			var notifications = await notificationService.GetNotificationsAsync(userId, page, pageSize);
			return Ok(notifications);
		}

		[HttpPost("mark-as-read")]
		public async Task<IActionResult> MarkAsReadAsync([FromBody] List<Guid> notificationIds)
		{
			await notificationService.MarkNotificationsAsReadAsync(notificationIds);
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Delete([FromQuery] string id)
		{
			string? userId = GetUserId();

			if (string.IsNullOrEmpty(userId))
			{
				return BadRequest();
			}

			bool isDeleted = await notificationService.DeleteAsync(userId, id);
			if (!isDeleted)
			{
				return BadRequest("Deletion failed");
			}

			return Ok("Deletion successfull");
		}
	}
}