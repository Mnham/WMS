using System;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes
{
    /// <summary>
    /// Представляет атрибут для указания оператора сравнения.
    /// </summary>
    public class SqlOperatorAttribute : Attribute
    {
        /// <summary>
        /// Оператор сравнения.
        /// </summary>
        public string Operator { get; }

        /// <summary>
        /// Создает экземпляр класса <see cref="SqlOperatorAttribute"/>.
        /// </summary>
        public SqlOperatorAttribute(string @operator) => Operator = @operator;
    }
}