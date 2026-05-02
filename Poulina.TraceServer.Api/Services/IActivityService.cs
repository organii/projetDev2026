using System;
using System.Threading.Tasks;

namespace AgileAi.Api.Services
{
    public interface IActivityService
    {
        Task Log(Guid projectId, string action, string entityType, Guid? entityId);
        Task Notify(Guid receiverId, string message, string link);
    }
}
