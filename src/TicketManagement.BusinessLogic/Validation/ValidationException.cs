using System;
using System.Runtime.Serialization;

namespace TicketManagement.BusinessLogic.Validation
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(string message, string prop)
            : base(message) => Property = prop;

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string Property { get; protected set; }
    }
}
