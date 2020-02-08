using System;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Domain
{
    public class Order : AggregateBase
    {
        public OrderStatus Status { get; private set; }
        public Order() { }

        public Order(Guid id)
        {
            var @event = new OrderCreated(id);
            Apply(@event);
            AddUncommittedEvent(@event);
        }

        public void Delivered()
        {
            var @statusChanged = new OrderStatusChanged(Id, OrderStatus.Delivered);
            Apply(@statusChanged);
            AddUncommittedEvent(@statusChanged);
        }

        public void Shipped()
        {
            var @statusChanged = new OrderStatusChanged(Id, OrderStatus.Shipped);
            Apply(@statusChanged);
            AddUncommittedEvent(@statusChanged);
        }

        protected void Apply(OrderCreated @event)
        {
            Id = @event.AggregateId;
        }

        protected void Apply(OrderStatusChanged @event)
        {
            Status = @event.Status;
        }
    }
}