using System;
using System.Threading;
using System.Threading.Tasks;
using webapi.src.Order.Application;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Domain
{
    public class FindOrderByIdQueryHandler : IQueryHandler<FindOrderByIdQuery, Order>
    {
        private readonly OrderFinder finder;

        public FindOrderByIdQueryHandler(IOrderRepository repository)
        {
            finder = new OrderFinder(repository);
        }

        public Task<Order> Handle(FindOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return finder.FindById(request.Id);
        }
    }
}