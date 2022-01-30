using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using WMS.ClassLibrary.Domain.Models;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts
{
    /// <summary>
    /// Интерфейс обработки запроса к базе данных.
    /// </summary>
    public interface IQueryExecutor
    {
        /// <summary>
        /// Обрабатывает запрос к базе данных.
        /// </summary>
        Task<T> Execute<T>(T entity, Func<Task> method) where T : Entity;

        /// <summary>
        /// Обрабатывает запрос к базе данных.
        /// </summary>
        Task<T> Execute<T>(Func<Task<T>> method) where T : Entity;

        /// <summary>
        /// Обрабатывает запрос к базе данных.
        /// </summary>
        Task<IEnumerable<T>> Execute<T>(Func<Task<IEnumerable<T>>> method) where T : Entity;
    }
}