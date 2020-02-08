using System;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Application
{
    public class OrderShippedCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}