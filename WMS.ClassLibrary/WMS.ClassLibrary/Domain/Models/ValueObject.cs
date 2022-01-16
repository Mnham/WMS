using System.Collections.Generic;
using System.Linq;

namespace WMS.ClassLibrary.Domain.Models
{
    public abstract class ValueObject
    {
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public ValueObject GetCopy() => MemberwiseClone() as ValueObject;

        public override int GetHashCode() => GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);

        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right) => !EqualOperator(left, right);

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}