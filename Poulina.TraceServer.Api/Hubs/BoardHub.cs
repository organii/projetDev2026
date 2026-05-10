using AgileAi.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace AgileAi.Api.Hubs
{
    [Authorize]
    public class BoardHub : Hub
    {
        private readonly ICurrentUserService _currentUser;

        public BoardHub(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public override async Task OnConnectedAsync()
        {
            if (_currentUser.UserId != Guid.Empty)
                await Groups.AddToGroupAsync(Context.ConnectionId, GetNotificationGroup(_currentUser.UserId));

            await base.OnConnectedAsync();
        }

        public Task JoinUserStory(Guid userStoryId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, userStoryId.ToString());
        }

        public Task LeaveUserStory(Guid userStoryId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, userStoryId.ToString());
        }

        public Task JoinIssue(Guid issueId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, issueId.ToString());
        }

        public Task LeaveIssue(Guid issueId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, issueId.ToString());
        }

        public static string GetNotificationGroup(Guid userId)
        {
            return $"notifications:{userId}";
        }
    }
}
