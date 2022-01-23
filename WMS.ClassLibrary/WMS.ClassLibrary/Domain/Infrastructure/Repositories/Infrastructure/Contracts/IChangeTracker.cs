using System.Collections.Generic;

using WMS.ClassLibrary.Domain.Models;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts
{
    /// <summary>
    /// Интерфейс отслеживания изменений.
    /// </summary>
    public interface IChangeTracker
    {
        /// <summary>
        /// Список сущностей.
        /// </summary>
        IEnumerable<Entity> TrackedEntities { get; }

        /// <summary>
        /// Добавляет сущность для отслеживания.
        /// </summary>
        public void Track(Entity entity);
    }
}