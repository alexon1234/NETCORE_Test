using System.Threading;
using System.Threading.Tasks;
using webapi.src.Order.Domain;
using webapi.src.Shared.Domain;

namespace webapi.src.Order.Application
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly OrderCreator creator;
        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            creator = new OrderCreator(repository);
        }


        protected override async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await creator.Create(request.Id);
        }
    }
}