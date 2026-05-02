using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Commands
{
    public class PredictPriorityCommand : IRequest<string>
    {
        public Guid UserStoryId { get; }
        public PredictPriorityCommand(Guid userStoryId) => UserStoryId = userStoryId;
    }
}
