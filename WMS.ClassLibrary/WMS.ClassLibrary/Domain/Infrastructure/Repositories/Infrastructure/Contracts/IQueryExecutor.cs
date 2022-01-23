using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using WMS.ClassLibrary.Domain.Models;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts
{
    /// <summary>
    /// ��������� ��������� ������� � ���� ������.
    /// </summary>
    public interface IQueryExecutor
    {
        /// <summary>
        /// ������������ ������ � ���� ������.
        /// </summary>
        Task<T> Execute<T>(T entity, Func<Task> method) where T : Entity;

        /// <summary>
        /// ������������ ������ � ���� ������.
        /// </summary>
        Task<T> Execute<T>(Func<Task<T>> method) where T : Entity;

        /// <summary>
        /// ������������ ������ � ���� ������.
        /// </summary>
        Task<IEnumerable<T>> Execute<T>(Func<Task<IEnumerable<T>>> method) where T : Entity;
    }
}