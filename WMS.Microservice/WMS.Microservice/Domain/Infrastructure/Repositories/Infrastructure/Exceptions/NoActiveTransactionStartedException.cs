using System;
using System.Runtime.Serialization;

namespace WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Exceptions
{
    /// <summary>
    /// Представляет исключение, возникающее при отсутствии запущенной транзакции.
    /// </summary>
    public class NoActiveTransactionStartedException : Exception
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="NoActiveTransactionStartedException"/>.
        /// </summary>
        public NoActiveTransactionStartedException()
        {
        }

        /// <summary>
        /// Создает экземпляр класса <see cref="NoActiveTransactionStartedException"/>.
        /// </summary>
        public NoActiveTransactionStartedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Создает экземпляр класса <see cref="NoActiveTransactionStartedException"/>.
        /// </summary>
        public NoActiveTransactionStartedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Создает экземпляр класса <see cref="NoActiveTransactionStartedException"/>.
        /// </summary>
        protected NoActiveTransactionStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}