using System;

namespace WMS.NomenclatureService.Domain.Exceptions
{
    /// <summary>
    /// Представляет исключение, возникающее, когда передаваемый аргумент меньше или равен нулю.
    /// </summary>
    public class NegativeOrZeroValueException : Exception
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="NegativeOrZeroValueException"/>.
        /// </summary>
        public NegativeOrZeroValueException(string message) : base(message)
        {
        }

        /// <summary>
        /// Создает экземпляр класса <see cref="NegativeOrZeroValueException"/>.
        /// </summary>
        public NegativeOrZeroValueException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}