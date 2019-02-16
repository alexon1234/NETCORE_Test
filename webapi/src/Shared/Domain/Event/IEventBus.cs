using System.Threading.Tasks;

namespace webapi.src.Shared.Domain.Event
{
    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}