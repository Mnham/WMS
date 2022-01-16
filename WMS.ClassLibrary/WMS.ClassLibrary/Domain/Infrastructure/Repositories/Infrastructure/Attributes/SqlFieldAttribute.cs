using System;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes
{
    public class SqlFieldAttribute : Attribute
    {
        public string FieldName { get; }

        public SqlFieldAttribute(string fieldName) => FieldName = fieldName;
    }
}