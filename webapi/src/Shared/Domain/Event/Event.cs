
using System;
using System.Text.Json;
using MediatR;

namespace webapi.src.Shared.Domain
{
    public abstract class Event : INotification
    {
        public Guid Id { get; private set; }
        public Guid AggregateId { get; private set; }
        public DateTime OccurredOn { get; private set; }

        public abstract string Name { get; }

        protected Event(Guid aggregateId, Guid? id = null, DateTime? occurredOn = null)
        {
            AggregateId = aggregateId;
            Id = id.HasValue ? id.Value : Guid.NewGuid();
            OccurredOn = occurredOn.HasValue ? occurredOn.Value : DateTime.Now;
        }
        protected Event(Guid aggregateId)
        {
            AggregateId = aggregateId;
            Id = Guid.NewGuid();
            OccurredOn = DateTime.Now;
        }
    }
}