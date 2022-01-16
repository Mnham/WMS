using System;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Attributes
{
    public class SqlOperatorAttribute : Attribute
    {
        public string Operator { get; }

        public SqlOperatorAttribute(string @operator) => Operator = @operator;
    }
}