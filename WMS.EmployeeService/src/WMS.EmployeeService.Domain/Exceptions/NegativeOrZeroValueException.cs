namespace WMS.EmployeeService.Domain.Exceptions
{
    /// <summary>
    /// Представляет исключение, возникающее, когда передаваемый аргумент меньше или равен нулю.
    /// </summary>
    public class NegativeOrZeroValueException : Exception
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="NegativeOrZeroValueException"/>.
        /// </summary>
        public NegativeOrZeroValueException(string name) :
            base($"{name} value must be positive.")
        {
        }

        /// <summary>
        /// Создает экземпляр класса <see cref="NegativeOrZeroValueException"/>.
        /// </summary>
        public NegativeOrZeroValueException(string name, Exception innerException) :
            base($"{name} value must be positive.", innerException)
        {
        }
    }
}