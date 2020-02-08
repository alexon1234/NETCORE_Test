using System;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Application
{
    public class CreateOrderCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}