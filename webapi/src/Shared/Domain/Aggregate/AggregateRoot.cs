using System;
using System.Collections.Generic;

namespace webapi.src.Shared.Domain
{
    // public abstract class AggregateRoot
    // {
    //     public Guid Id { get; private set; }
    //     private readonly Queue<Event> _domainEvents;

    //     protected AggregateRoot(Guid id)
    //     {
    //         Id = id;
    //         _domainEvents = new Queue<Event>();
    //     }

    //     public virtual Event[] PullDomainEvents()
    //     {
    //         var domainEvents = _domainEvents.ToArray();
    //         _domainEvents.Clear();
    //         return domainEvents;
    //     }

    //     protected void Record(Event notification)
    //     {
    //         _domainEvents.Enqueue(notification);
    //     }
    // }
}