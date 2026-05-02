using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Queries
{
    public class PredictVelocityQuery : IRequest<double>
    {
        public Guid ProjectId { get; }
        public PredictVelocityQuery(Guid projectId) => ProjectId = projectId;
    }
}
