using System.Threading;
using System.Threading.Tasks;
using webapi.src.Order.Domain;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Application
{
    public class UpdateOrderStatusCommandHandler : ICommandHandler<UpdateOrderStatusCommand>
    {
        private readonly OrderUpdater updater;

        public UpdateOrderStatusCommandHandler(IOrderRepository repo)
        {
            updater = new OrderUpdater(repo);
        }

        protected override async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            await updater.Update(request.Id, request.Status);
        }
    }
}