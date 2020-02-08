using System;
using System.Collections.Generic;

namespace webapi.src.Shared.Domain
{
    public abstract class AggregateBase
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        private readonly List<object> _uncommittedEvents = new List<object>();

        public IEnumerable<object> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        protected void AddUncommittedEvent(object @event)
        {
            _uncommittedEvents.Add(@event);
        }
    }


}