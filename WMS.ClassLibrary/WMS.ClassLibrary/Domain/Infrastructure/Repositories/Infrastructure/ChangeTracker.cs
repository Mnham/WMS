using System.Collections.Concurrent;
using System.Collections.Generic;

using WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts;
using WMS.ClassLibrary.Domain.Models;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure
{
    /// <summary>
    /// ������������ ������������ ���������.
    /// </summary>
    public class ChangeTracker : IChangeTracker
    {
        /// <summary>
        /// ������ ���������.
        /// </summary>
        private readonly ConcurrentBag<Entity> _usedEntitiesBackingField = new();

        /// <summary>
        /// ������ ���������.
        /// </summary>
        public IEnumerable<Entity> TrackedEntities => _usedEntitiesBackingField.ToArray();

        /// <summary>
        /// ��������� �������� ��� ������������.
        /// </summary>
        public void Track(Entity entity) => _usedEntitiesBackingField.Add(entity);
    }
}