using MediatR;

using System;
using System.Collections.Generic;

namespace WMS.Microservice.Domain.Models
{
    /// <summary>
    /// Представляет сущность доменного уровня.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Список событий.
        /// </summary>
        private readonly List<INotification> _domainEvents = new();

        /// <summary>
        /// 
        /// </summary>
        private int? _requestedHashCode;

        /// <summary>
        /// Список событий.
        /// </summary>
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public virtual long Id { get; protected set; }

        /// <summary>
        /// Оператор сравнения.
        /// </summary>
        public static bool operator !=(Entity left, Entity right) => !(left == right);

        /// <summary>
        /// Оператор сравнения.
        /// </summary>
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

        /// <summary>
        /// Добавляет событие.
        /// </summary>
        public void AddDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);

        /// <summary>
        /// Удаляет все события.
        /// </summary>
        public void ClearDomainEvents() => _domainEvents?.Clear();

        /// <summary>
        /// Переопределенный метод Equals.
        /// </summary>
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

        /// <summary>
        /// Переопределенный метод GetHashCode.
        /// </summary>
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

        /// <summary>
        /// Возвращает <see langword="true"/>, если идентификатор не определен, иначе <see langword="false"/>.
        /// </summary>
        public bool IsTransient() => Id == default;

        /// <summary>
        /// Удаляет событие.
        /// </summary>
        public void RemoveDomainEvent(INotification eventItem) => _domainEvents?.Remove(eventItem);

        /// <summary>
        /// Устанавливает идентификатор.
        /// </summary>
        public void SetId(long id)
        {
            if (IsTransient())
            {
                Id = id;
            }
        }
    }
}