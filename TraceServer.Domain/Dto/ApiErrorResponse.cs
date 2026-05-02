using System.Collections.Generic;

namespace AgileAi.Domain.Dto
{
    public class ApiErrorResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
