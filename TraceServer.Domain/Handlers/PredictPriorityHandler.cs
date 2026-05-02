using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgileAi.Domain.Commands;

namespace AgileAi.Domain.Handlers
{
    public class PredictPriorityHandler : IRequestHandler<PredictPriorityCommand, string>
    {
        // Inject your ML.NET Service and Repository here
        public async Task<string> Handle(PredictPriorityCommand request, CancellationToken cancellationToken)
        {
            // 1. Get the User Story from DB
            // 2. Pass parameters (Description, Points) to ML.NET Model
            // 3. Return prediction (e.g., "High", "Medium", "Low")
            return await Task.FromResult("High"); // Placeholder
        }
    }
}
