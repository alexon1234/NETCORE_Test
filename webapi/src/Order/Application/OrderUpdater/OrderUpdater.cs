using System;
using System.Threading.Tasks;
using webapi.src.Order.Domain;

namespace webapi.src.Order.Application
{
    public class OrderUpdater
    {
        private readonly IOrderRepository _repo;

        public OrderUpdater(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task Update(Guid orderId, OrderStatus status)
        {
            var order = await _repo.Load(orderId);
            order.Delivered();
            await _repo.Store(order);
        }
    }
}