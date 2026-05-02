using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace AgileAi.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var value = User?.FindFirstValue("UserId");
                return Guid.TryParse(value, out var userId) ? userId : Guid.Empty;
            }
        }

        public string Email => User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

        public string Role => User?.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated == true;

        public bool IsAdmin => string.Equals(Role, "admin", StringComparison.OrdinalIgnoreCase);

        private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;
    }
}
