using System;
using System.Threading.Tasks;

namespace webapi.src.Order.Domain
{
    public interface IOrderRepository
    {
        Task Store(Order order);
        Task<Order> Load(Guid id, int? version = null);
    }

}