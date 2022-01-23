using System;
using System.Threading;
using System.Threading.Tasks;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts
{
    /// <summary>
    /// Интерфейс обработки транзакции.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Начинает транзакцию.
        /// </summary>
        ValueTask StartTransaction(CancellationToken token);

        /// <summary>
        /// Сохраняет изменения.
        /// </summary>
        Task SaveChanges(CancellationToken token);
    }
}