using Marten;
using webapi.src.Order.Domain;
using webapi.src.Shared.Infrastructure;

namespace webapi.src.Order.Infrastructure
{
    public class OrderRepository : AggregateRepository<Order.Domain.Order>, IOrderRepository
    {
        private readonly string connString;

        public OrderRepository(string connString = "host=192.168.99.103;database=postgres;password=mysecretpassword;username=postgres") :
        base(DocumentStore.For(connString))
        {
            this.connString = connString;
        }
    }
};