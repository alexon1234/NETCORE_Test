using System;
using System.Threading.Tasks;
using webapi.src.Order.Domain;


namespace webapi.src.Order.Application
{
    public class OrderCreator
    {
        private readonly IOrderRepository repository;

        public OrderCreator(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public Task Create(Guid id)
        {
            var order = new Order.Domain.Order(id);
            return repository.Store(order);
        }
    }
}