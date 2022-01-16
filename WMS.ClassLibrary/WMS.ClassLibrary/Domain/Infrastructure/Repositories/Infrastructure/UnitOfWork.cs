using MediatR;

using Microsoft.Extensions.Options;

using Npgsql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WMS.ClassLibrary.Domain.Infrastructure.Configuration;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Exceptions;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IChangeTracker _changeTracker;
        private readonly DatabaseConnectionOptions _options;
        private readonly IPublisher _publisher;
        private NpgsqlTransaction _npgsqlTransaction;

        public UnitOfWork(
            IOptions<DatabaseConnectionOptions> options,
            IPublisher publisher,
            IChangeTracker changeTracker)
        {
            _options = options.Value;
            _publisher = publisher;
            _changeTracker = changeTracker;
        }

        void IDisposable.Dispose() => _npgsqlTransaction?.Dispose();

        public async Task SaveChanges(CancellationToken token)
        {
            if (_npgsqlTransaction is null)
            {
                throw new NoActiveTransactionStartedException();
            }

            Queue<INotification> domainEvents = new(
                _changeTracker.TrackedEntities
                    .SelectMany(x =>
                    {
                        List<INotification> events = x.DomainEvents.ToList();
                        x.ClearDomainEvents();

                        return events;
                    }));

            while (domainEvents.TryDequeue(out INotification notification))
            {
                await _publisher.Publish(notification, token);
            }

            await _npgsqlTransaction.CommitAsync(token);
        }

        public async ValueTask StartTransaction(CancellationToken token)
        {
            if (_npgsqlTransaction is not null)
            {
                return;
            }

            using NpgsqlConnection connection = new(_options.ConnectionString);
            await connection.OpenAsync(token);
            _npgsqlTransaction = await connection.BeginTransactionAsync(token);
        }
    }
}