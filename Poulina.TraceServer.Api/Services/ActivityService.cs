using System;
using System.Threading.Tasks;
using AgileAi.Api.Hubs;
using AgileAi.Data.Context;
using AgileAi.Domain.Dto;
using AgileAi.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace AgileAi.Api.Services
{
    public class ActivityService : IActivityService
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IHubContext<BoardHub> _boardHub;

        public ActivityService(
            AppDbContext context,
            ICurrentUserService currentUser,
            IHubContext<BoardHub> boardHub)
        {
            _context = context;
            _currentUser = currentUser;
            _boardHub = boardHub;
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
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid(),
                ReceiverId = receiverId,
                Message = message,
                Link = link,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            await _boardHub.Clients
                .Group(BoardHub.GetNotificationGroup(receiverId))
                .SendAsync("NotificationReceived", ToResponse(notification));
        }

        private static NotificationResponseDto ToResponse(Notification notification)
        {
            return new NotificationResponseDto
            {
                NotificationId = notification.NotificationId,
                Message = notification.Message,
                Link = notification.Link,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt,
                ReceiverId = notification.ReceiverId
            };
        }
    }
}
