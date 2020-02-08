using System.Threading.Tasks;

namespace webapi.src.Shared.Domain
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}