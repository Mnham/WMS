using System;
using System.Runtime.Serialization;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Exceptions
{
    public class NoActiveTransactionStartedException : Exception
    {
        public NoActiveTransactionStartedException()
        {
        }

        public NoActiveTransactionStartedException(string message) : base(message)
        {
        }

        public NoActiveTransactionStartedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoActiveTransactionStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}