using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileAi.Domain.Commands
{
    public class GenerateDescriptionCommand : IRequest<string>
    {
        public string Title { get; }
        public GenerateDescriptionCommand(string title) => Title = title;
    }
}
