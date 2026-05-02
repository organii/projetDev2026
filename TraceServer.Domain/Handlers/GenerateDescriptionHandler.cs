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
    public class GenerateDescriptionHandler : IRequestHandler<GenerateDescriptionCommand, string>
    {
        public async Task<string> Handle(GenerateDescriptionCommand request, CancellationToken cancellationToken)
        {
            // 1. Call OpenAI / Mistral / Ollama API
            // 2. Prompt: "As an agile expert, write a user story description for the title: {request.Title}"
            // 3. Return the generated text
            var generatedText = "As a user, I want to... so that..."; // Placeholder
            return await Task.FromResult(generatedText);
        }
    }
}
