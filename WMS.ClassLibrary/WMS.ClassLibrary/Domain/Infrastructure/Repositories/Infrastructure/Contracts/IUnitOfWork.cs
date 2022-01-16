using System;
using System.Threading;
using System.Threading.Tasks;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask StartTransaction(CancellationToken token);

        Task SaveChanges(CancellationToken token);
    }
}