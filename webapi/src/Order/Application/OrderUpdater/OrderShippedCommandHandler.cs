using System;
using System.Threading;
using System.Threading.Tasks;
using webapi.src.Order.Domain;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Application
{
    public class OrderShippedCommandHandler : ICommandHandler<OrderShippedCommand>
    {
        private readonly OrderFinder finder;
        private readonly OrderUpdater updater;

        public OrderShippedCommandHandler(IOrderRepository repo)
        {
            finder = new OrderFinder(repo);
            updater = new OrderUpdater(repo);
        }

        protected override async Task Handle(OrderShippedCommand request, CancellationToken cancellationToken)
        {
            var order = await finder.FindById(request.Id);
            order.Shipped();
            await updater.Update(order);
        }
    }
}