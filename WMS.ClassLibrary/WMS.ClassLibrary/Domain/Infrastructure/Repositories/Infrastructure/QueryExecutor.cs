using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.ClassLibrary.Domain.Models;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure
{
    /// <summary>
    /// ѕредставл€ет обработку запроса к базе данных.
    /// </summary>
    public class QueryExecutor : IQueryExecutor
    {
        /// <summary>
        /// Ёкземпл€р класса дл€ отслеживани€ изменений.
        /// </summary>
        private readonly IChangeTracker _changeTracker;

        /// <summary>
        /// —оздает экземпл€р класса <see cref="QueryExecutor"/>.
        /// </summary>
        public QueryExecutor(IChangeTracker changeTracker) => _changeTracker = changeTracker;

        /// <summary>
        /// ќбрабатывает запрос к базе данных.
        /// </summary>
        public async Task<T> Execute<T>(T entity, Func<Task> method) where T : Entity
        {
            await method();
            _changeTracker.Track(entity);

            return entity;
        }

        /// <summary>
        /// ќбрабатывает запрос к базе данных.
        /// </summary>
        public async Task<T> Execute<T>(Func<Task<T>> method) where T : Entity
        {
            T result = await method();
            _changeTracker.Track(result);

            return result;
        }

        /// <summary>
        /// ќбрабатывает запрос к базе данных.
        /// </summary>
        public async Task<IEnumerable<T>> Execute<T>(Func<Task<IEnumerable<T>>> method) where T : Entity
        {
            List<T> result = (await method()).ToList();
            foreach (T entity in result)
            {
                _changeTracker.Track(entity);
            }

            return result;
        }
    }
}