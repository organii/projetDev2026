using System;

namespace AgileAi.Domain.Exceptions
{
    public class ResourceConflictException : Exception
    {
        public ResourceConflictException(string message, string code = "RESOURCE_CONFLICT")
            : base(message)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
