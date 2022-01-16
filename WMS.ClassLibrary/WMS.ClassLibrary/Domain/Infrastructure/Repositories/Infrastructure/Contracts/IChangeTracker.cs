using System.Collections.Generic;

using WMS.ClassLibrary.Domain.Models;

namespace WMS.ClassLibrary.Domain.Infrastructure.Repositories.Infrastructure.Contracts
{
    public interface IChangeTracker
    {
        IEnumerable<Entity> TrackedEntities { get; }

        public void Track(Entity entity);
    }
}