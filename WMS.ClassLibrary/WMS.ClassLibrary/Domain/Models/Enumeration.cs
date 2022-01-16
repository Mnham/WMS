using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WMS.ClassLibrary.Domain.Models
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; }
        public string Name { get; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
            => typeof(T).GetFields(BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

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

        public override string ToString() => Name;
    }
}