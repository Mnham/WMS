using MediatR;

using System;
using System.Collections.Generic;

namespace WMS.ClassLibrary.Domain.Models
{
    public abstract class Entity
    {
        private readonly List<INotification> _domainEvents = new();
        private int? _requestedHashCode;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
        public virtual long Id { get; protected set; }

        public static bool operator !=(Entity left, Entity right) => !(left == right);

        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }
            else
            {
                return left.Equals(right);
            }
        }

        public void AddDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);

        public void ClearDomainEvents() => _domainEvents?.Clear();

        public override bool Equals(object obj)
        {
            if (obj is not Entity entity)
            {
                return false;
            }

            if (ReferenceEquals(this, entity))
            {
                return true;
            }

            if (GetType() != entity.GetType())
            {
                return false;
            }

            if (entity.IsTransient() || IsTransient())
            {
                return false;
            }
            else
            {
                return entity.Id == Id;
            }
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = HashCode.Combine(Id, 31);
                }

                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public bool IsTransient() => Id == default;

        public void RemoveDomainEvent(INotification eventItem) => _domainEvents?.Remove(eventItem);

        public void SetId(long id)
        {
            if (IsTransient())
            {
                Id = id;
            }
        }
    }
}