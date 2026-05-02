using System;
using System.Collections.Generic;

namespace AgileAi.Domain.Dto
{
    public class GenerateDescriptionRequestDto
    {
        public string Title { get; set; }
        public Guid? ProjectId { get; set; }
    }

    public class GenerateSubTasksRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class AiPredictionResponseDto
    {
        public string PredictionType { get; set; }
        public string SuggestedValue { get; set; }
        public double ConfidenceScore { get; set; }
        public IEnumerable<string> Suggestions { get; set; }
    }
}
