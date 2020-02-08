using System.Threading.Tasks;

namespace webapi.src.Shared.Domain
{
    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : Event;
    }
}