using System;
using System.Runtime.Serialization;

namespace TicketManagement.Common.Validation
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(string message, string prop)
            : base(message) => Property = prop;

        public ValidationException()
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string Property { get; protected set; }
    }
}
