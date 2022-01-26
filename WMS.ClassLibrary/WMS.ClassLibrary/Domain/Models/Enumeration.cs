using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WMS.ClassLibrary.Domain.Models
{
    /// <summary>
    /// Представляет перечисление.
    /// </summary>
    public abstract class Enumeration : IComparable
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Создает экземпляр класса <see cref="Enumeration"/>.
        /// </summary>
        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        /// <summary>
        /// Возвращает все значения перечисления.
        /// </summary>
        public static IEnumerable<T> GetAll<T>() where T : Enumeration
            => typeof(T).GetFields(BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();

        /// <summary>
        /// Возвращает результат сравнения.
        /// </summary>
        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        /// <summary>
        /// Переопределенный метод Equals.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            bool typeMatches = GetType().Equals(obj.GetType());
            bool valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        /// <summary>
        /// Переопределенный метод ToString.
        /// </summary>
        public override string ToString() => Name;
    }
}