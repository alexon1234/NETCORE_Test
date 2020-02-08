using System.Threading;
using System.Threading.Tasks;
using webapi.src.Order.Domain;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Application
{
    public class OrderDeliveredCommandHandler : ICommandHandler<OrderDeliveredCommand>
    {
        private readonly OrderFinder finder;
        private readonly OrderUpdater updater;

        public OrderDeliveredCommandHandler(IOrderRepository repo)
        {
            finder = new OrderFinder(repo);
            updater = new OrderUpdater(repo);
        }

        protected override async Task Handle(OrderDeliveredCommand request, CancellationToken cancellationToken)
        {
            var order = await finder.FindById(request.Id);
            order.Delivered();
            await updater.Update(order);

        }
    }
}