using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure
{
    /// <summary>
    /// Представляет параметр фильтрации.
    /// </summary>
    public class FilterParameter
    {
        /// <summary>
        /// Имя параметра.
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// Имя поля Sql-таблицы.
        /// </summary>
        public string SqlField { get; }

        /// <summary>
        /// Оператор сравнения.
        /// </summary>
        public string SqlOperator { get; }

        /// <summary>
        /// Значение.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Создает экземпляр класса <see cref="FilterParameter"/>.
        /// </summary>
        public FilterParameter(string sqlField, object value, string parameterName, string sqlOperator)
        {
            SqlField = sqlField;
            Value = value;
            ParameterName = parameterName;
            SqlOperator = sqlOperator;
        }

        /// <summary>
        /// Возвращает все параметры фильтра.
        /// </summary>
        public static IReadOnlyList<FilterParameter> GetFilters<T>(T obj) =>
            typeof(T)
                .GetProperties()
                .Where(p => p.GetValue(obj) is not null)
                .Select(p => new FilterParameter(GetSqlField(p), p.GetValue(obj), p.Name, GetSqlOperator(p)))
                .ToList();

        /// <summary>
        /// Возвращает имя поля.
        /// </summary>
        private static string GetSqlField(PropertyInfo propertyInfo) =>
            propertyInfo.GetCustomAttribute<SqlFieldAttribute>().FieldName;

        /// <summary>
        /// Возвращает оператор сравнения.
        /// </summary>
        private static string GetSqlOperator(PropertyInfo propertyInfo) =>
            propertyInfo.GetCustomAttribute<SqlOperatorAttribute>().Operator;
    }
}