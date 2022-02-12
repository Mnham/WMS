using System.Collections.Concurrent;
using System.Collections.Generic;

using WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.Microservice.Domain.Models;

namespace WMS.Microservice.Domain.Infrastructure.Repositories.Infrastructure
{
    /// <summary>
    /// Представляет отслеживание изменений.
    /// </summary>
    public class ChangeTracker : IChangeTracker
    {
        /// <summary>
        /// Список сущностей.
        /// </summary>
        private readonly ConcurrentBag<Entity> _usedEntitiesBackingField = new();

        /// <summary>
        /// Список сущностей.
        /// </summary>
        public IEnumerable<Entity> TrackedEntities => _usedEntitiesBackingField.ToArray();

        /// <summary>
        /// Добавляет сущность для отслеживания.
        /// </summary>
        public void Track(Entity entity) => _usedEntitiesBackingField.Add(entity);
    }
}