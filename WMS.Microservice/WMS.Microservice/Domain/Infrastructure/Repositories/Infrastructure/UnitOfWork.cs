using MediatR;

using Microsoft.Extensions.Options;

using Npgsql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WMS.Microservice.Domain.Infrastructure.Configuration;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Exceptions;
using WMS.Microservice.Domain.Models;

namespace WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure
{
    /// <summary>
    /// Представляет обработку транзакции.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Список сущностей.
        /// </summary>
        private readonly IEnumerable<Entity> _trackedEntities;

        /// <summary>
        /// Настройки подключения к базе данных.
        /// </summary>
        private readonly DatabaseConnectionOptions _options;

        /// <summary>
        /// Экземпляр класса для публикации уведомлений.
        /// </summary>
        private readonly IPublisher _publisher;

        /// <summary>
        /// Транзакция.
        /// </summary>
        private NpgsqlTransaction _npgsqlTransaction;

        /// <summary>
        /// Создает экземпляр класса <see cref="UnitOfWork"/>.
        /// </summary>
        public UnitOfWork(
            IOptions<DatabaseConnectionOptions> options,
            IPublisher publisher,
            IChangeTracker changeTracker)
        {
            _options = options.Value;
            _publisher = publisher;
            _trackedEntities = changeTracker.TrackedEntities;
        }

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
        void IDisposable.Dispose() => _npgsqlTransaction?.Dispose();

        /// <summary>
        /// Сохраняет изменения.
        /// </summary>
        public async Task SaveChanges(CancellationToken token)
        {
            if (_npgsqlTransaction is null)
            {
                throw new NoActiveTransactionStartedException();
            }

            Queue<INotification> domainEvents = new(
                _trackedEntities
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

        /// <summary>
        /// Начинает транзакцию.
        /// </summary>
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