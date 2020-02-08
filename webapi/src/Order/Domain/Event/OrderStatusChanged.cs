using System;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Domain
{
    public class OrderStatusChanged : Event
    {
        public OrderStatusChanged(Guid aggregateId, OrderStatus status) : base(aggregateId, null, null)
        {
            Status = status;
        }

        public OrderStatus Status { get; set; }

        public override string Name => "order.statusChanged";
    }
}