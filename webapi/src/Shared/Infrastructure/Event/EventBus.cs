using System.Threading.Tasks;
using MediatR;
using webapi.src.Shared.Domain;

namespace webapi.src.Shared.Infrastructure
{
    public class EventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish<TEvent>(params TEvent[] events) where TEvent : Event
        {
            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
            }
        }
    }
}