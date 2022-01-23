using System.Collections.Generic;

using WMS.ClassLibrary.Domain.Models;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts
{
    /// <summary>
    /// ��������� ������������ ���������.
    /// </summary>
    public interface IChangeTracker
    {
        /// <summary>
        /// ������ ���������.
        /// </summary>
        IEnumerable<Entity> TrackedEntities { get; }

        /// <summary>
        /// ��������� �������� ��� ������������.
        /// </summary>
        public void Track(Entity entity);
    }
}