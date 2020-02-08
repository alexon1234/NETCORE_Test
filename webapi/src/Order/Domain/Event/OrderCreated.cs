
using System;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Domain
{
    public class OrderCreated : Event
    {
        public OrderCreated(Guid aggregateId, Guid? id = null, DateTime? occurredOn = null) : base(aggregateId, id, occurredOn)
        {
        }

        public override string Name => "order.created";
    }
}