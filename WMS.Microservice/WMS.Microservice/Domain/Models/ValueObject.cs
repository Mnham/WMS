using System.Collections.Generic;
using System.Linq;

namespace WMS.Microservice.Domain.Models
{
    /// <summary>
    /// Представляет объект значения.
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Переопределенный метод Equals.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Возвращает копию экземпляра.
        /// </summary>
        public ValueObject GetCopy() => MemberwiseClone() as ValueObject;

        /// <summary>
        /// Переопределенный метод GetHashCode.
        /// </summary>
        public override int GetHashCode() => GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);

        /// <summary>
        /// Сравнивает два экземпляра <see cref="ValueObject"/> и возвращает <see langword="true"/>, если экземпляры равны, иначе <see langword="false"/>.
        /// </summary>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.Equals(right);
        }

        /// <summary>
        /// Сравнивает два экземпляра <see cref="ValueObject"/> и возвращает <see langword="true"/>, если экземпляры не равны, иначе <see langword="false"/>.
        /// </summary>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right) => !EqualOperator(left, right);

        /// <summary>
        /// Возвращает значения свойств для сравнения экземпляров <see cref="ValueObject"/>.
        /// </summary>
        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}