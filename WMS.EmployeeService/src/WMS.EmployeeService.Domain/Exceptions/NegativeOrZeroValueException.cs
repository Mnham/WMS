namespace WMS.EmployeeService.Domain.Exceptions
{
    /// <summary>
    /// Представляет исключение, возникающее, когда передаваемый аргумент меньше или равен нулю.
    /// </summary>
    public class NegativeOrZeroValueException : Exception
    {
        private const string MESSAGE = "{0} value must be positive.";

        /// <summary>
        /// Создает экземпляр класса <see cref="NegativeOrZeroValueException"/>.
        /// </summary>
        public NegativeOrZeroValueException(string name) : base(string.Format(MESSAGE, name))
        {
        }

        /// <summary>
        /// Создает экземпляр класса <see cref="NegativeOrZeroValueException"/>.
        /// </summary>
        public NegativeOrZeroValueException(string name, Exception innerException) : base(string.Format(MESSAGE, name), innerException)
        {
        }
    }
}