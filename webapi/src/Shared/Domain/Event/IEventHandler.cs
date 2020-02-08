using MediatR;

namespace webapi.src.Shared.Domain
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : Event
    {

    }
}