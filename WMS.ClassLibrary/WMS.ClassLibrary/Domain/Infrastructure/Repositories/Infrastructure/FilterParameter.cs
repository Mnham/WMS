using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure
{
    public class FilterParameter
    {
        public string ParameterName { get; }

        public string SqlField { get; }

        public string SqlOperator { get; }

        public object Value { get; }

        public FilterParameter(string sqlField, object value, string parameterName, string sqlOperator)
        {
            SqlField = sqlField;
            Value = value;
            ParameterName = parameterName;
            SqlOperator = sqlOperator;
        }

        public static IReadOnlyList<FilterParameter> GetFilters<T>(T obj) =>
            typeof(T)
                .GetProperties()
                .Where(p => p.GetValue(obj) is not null)
                .Select(p => new FilterParameter(GetSqlField(p), p.GetValue(obj), p.Name, GetSqlOperator(p)))
                .ToList();

        private static string GetSqlField(PropertyInfo propertyInfo) =>
            propertyInfo.GetCustomAttribute<SqlFieldAttribute>().FieldName;

        private static string GetSqlOperator(PropertyInfo propertyInfo) =>
            propertyInfo.GetCustomAttribute<SqlOperatorAttribute>().Operator;
    }
}