using System;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Domain
{
    public class FindOrderByIdQuery : IQuery<Order>
    {
        public Guid Id { get; set; }
    }
}