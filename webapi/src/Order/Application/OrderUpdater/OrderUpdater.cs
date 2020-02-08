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

        public async Task Update(Order.Domain.Order order)
        {
            await _repo.Store(order);
        }
    }
}