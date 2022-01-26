using System;
using System.Runtime.Serialization;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Exceptions
{
    /// <summary>
    /// ������������ ����������, ����������� ��� ���������� ���������� ����������.
    /// </summary>
    public class NoActiveTransactionStartedException : Exception
    {
        /// <summary>
        /// ������� ��������� ������ <see cref="NoActiveTransactionStartedException"/>.
        /// </summary>
        public NoActiveTransactionStartedException()
        {
        }

        /// <summary>
        /// ������� ��������� ������ <see cref="NoActiveTransactionStartedException"/>.
        /// </summary>
        public NoActiveTransactionStartedException(string message) : base(message)
        {
        }

        /// <summary>
        /// ������� ��������� ������ <see cref="NoActiveTransactionStartedException"/>.
        /// </summary>
        public NoActiveTransactionStartedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// ������� ��������� ������ <see cref="NoActiveTransactionStartedException"/>.
        /// </summary>
        protected NoActiveTransactionStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}