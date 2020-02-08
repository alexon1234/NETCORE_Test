using System;
using System.Threading.Tasks;
using webapi.src.Order.Domain;

namespace webapi.src.Order.Application
{
    public class OrderFinder
    {
        private readonly IOrderRepository repository;

        public OrderFinder(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Order.Domain.Order> FindById(Guid id)
        {
            return await this.repository.Load(id);
        }
    }
}