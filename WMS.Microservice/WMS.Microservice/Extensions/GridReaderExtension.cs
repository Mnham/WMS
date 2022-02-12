using System;
using System.Collections.Generic;
using System.Linq;

using static Dapper.SqlMapper;

namespace WMS.Microservice.Extensions
{
    /// <summary>
    /// Представляет методы расширения для <see cref="GridReader"/>.
    /// </summary>
    public static class GridReaderExtension
    {
        /// <summary>
        /// Извлекает сущности и устанавливает связь между зависимыми сущностями.
        /// </summary>
        public static IEnumerable<TFirst> Map<TFirst, TSecond, TKey>(
            this GridReader reader,
            Func<TFirst, TKey> firstKey,
            Func<TSecond, TKey> secondKey,
            Action<TFirst, IEnumerable<TSecond>> addChildren)
        {
            List<TFirst> first = reader.Read<TFirst>().ToList();
            Dictionary<TKey, IEnumerable<TSecond>> childMap = reader
                .Read<TSecond>()
                .GroupBy(s => secondKey(s))
                .ToDictionary(g => g.Key, g => g.AsEnumerable());

            foreach (TFirst item in first)
            {
                if (childMap.TryGetValue(firstKey(item), out IEnumerable<TSecond> children))
                {
                    addChildren(item, children);
                }
            }

            return first;
        }
    }
}