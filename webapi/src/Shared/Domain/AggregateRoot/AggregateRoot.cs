using System.Collections.Generic;
using webapi.src.Shared.Domain.Event;

namespace webapi.src.Shared.Domain.AggregateRoot
{
    public abstract class AggregateRoot
    {
        private readonly Queue<IEvent> _domainEvents;

        protected AggregateRoot()
        {
            _domainEvents = new Queue<IEvent>();
        }

        public virtual IEvent[] PullDomainEvents()
        {
            var domainEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return domainEvents;
        }

        protected void Record(IEvent notification)
        {
            _domainEvents.Enqueue(notification);
        }
    }
}