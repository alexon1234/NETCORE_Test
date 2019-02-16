using MediatR;

namespace webapi.src.Shared.Domain.Event
{
    public interface IEventHandler<in TEvent>: INotificationHandler<TEvent> where TEvent: IEvent
    {
        
    }
}