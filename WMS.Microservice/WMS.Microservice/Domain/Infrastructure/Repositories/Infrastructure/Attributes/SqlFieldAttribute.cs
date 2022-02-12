using System;

namespace WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Attributes
{
    /// <summary>
    /// Представляет атрибут для указания имени поля SQL-таблицы.
    /// </summary>
    public class SqlFieldAttribute : Attribute
    {
        /// <summary>
        /// Имя поля.
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Создает экземпляр класса <see cref="SqlFieldAttribute"/>.
        /// </summary>
        public SqlFieldAttribute(string fieldName) => FieldName = fieldName;
    }
}