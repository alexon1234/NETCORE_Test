using System;
using webapi.src.Order.Domain;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Application
{
    public class OrderDeliveredCommand : ICommand
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}