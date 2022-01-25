using System;
using System.Collections.Generic;

namespace WMS.ClassLibrary.Extensions
{
    /// <summary>
    /// Представляет методы расширения для <see cref="IEnumerable{T}"/>.    
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Преобразует перечисление одного типа в перечисление другого типа, используя <paramref name="mapper"/>.
        /// </summary>
        public static IEnumerable<Out> Map<In, Out>(this IEnumerable<In> source, Func<In, Out> mapper)
        {
            foreach (In item in source)
            {
                yield return mapper(item);
            }
        }
    }
}