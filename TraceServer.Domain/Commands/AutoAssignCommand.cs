using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgileAi.Domain.Models;

namespace AgileAi.Domain.Commands
{
    public class AutoAssignCommand : IRequest<Issue>
    {
        public Guid IssueId { get; }
        public AutoAssignCommand(Guid issueId) => IssueId = issueId;
    }
}
