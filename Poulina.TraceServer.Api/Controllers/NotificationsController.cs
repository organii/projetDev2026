using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgileAi.Api.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgileAi.Data.Context;
using AgileAi.Domain.Dto;

namespace AgileAi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public NotificationsController(AppDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMine()
        {
            var userId = _currentUser.UserId;
            var notifications = await _context.Notifications
                .Where(n => n.ReceiverId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NotificationResponseDto
                {
                    NotificationId = n.NotificationId,
                    Message = n.Message,
                    Link = n.Link,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt,
                    ReceiverId = n.ReceiverId
                })
                .ToListAsync();

            return Ok(notifications);
        }

        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkRead(Guid id)
        {
            var userId = _currentUser.UserId;
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationId == id && n.ReceiverId == userId);

            if (notification == null)
                return NotFound();

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
