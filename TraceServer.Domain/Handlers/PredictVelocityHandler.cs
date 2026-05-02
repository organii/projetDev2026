using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgileAi.Domain.Interfaces;
using AgileAi.Domain.Models;
using AgileAi.Domain.Queries;

namespace AgileAi.Domain.Handlers
{
    public class PredictVelocityHandler : IRequestHandler<PredictVelocityQuery, double>
    {
        private readonly IGenericRepository<Sprint> _sprintRepository;

        public PredictVelocityHandler(IGenericRepository<Sprint> sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }

        public async Task<double> Handle(PredictVelocityQuery request, CancellationToken cancellationToken)
        {
            // Logic: Calculate the average story points completed in past sprints
            var pastSprints = _sprintRepository.GetList(s => s.ProjectId == request.ProjectId && s.Status == (ItemStatus)5);

            if (!pastSprints.Any()) return 0;

            // Simple average for the gala demo
            double averageVelocity = pastSprints.Average(s => s.CompletedPoints);
            return await Task.FromResult(averageVelocity);
        }
    }
}
