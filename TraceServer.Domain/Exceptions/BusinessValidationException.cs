using System;

namespace AgileAi.Domain.Exceptions
{
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException(string message, string code = "VALIDATION_ERROR")
            : base(message)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
