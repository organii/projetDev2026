using System;
using System.Threading.Tasks;
using AgileAi.Data.Context;
using AgileAi.Domain.Models;

namespace AgileAi.Api.Services
{
    public class ActivityService : IActivityService
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public ActivityService(AppDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task Log(Guid projectId, string action, string entityType, Guid? entityId)
        {
            _context.ActivityLogs.Add(new ActivityLog
            {
                ActivityLogId = Guid.NewGuid(),
                ProjectId = projectId,
                ActorId = _currentUser.UserId == Guid.Empty ? null : _currentUser.UserId,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        public async Task Notify(Guid receiverId, string message, string link)
        {
            _context.Notifications.Add(new Notification
            {
                NotificationId = Guid.NewGuid(),
                ReceiverId = receiverId,
                Message = message,
                Link = link,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }
    }
}
