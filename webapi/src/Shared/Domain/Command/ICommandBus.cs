using System.Threading.Tasks;

namespace webapi.src.Shared.Domain.Command
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand: ICommand;
    }
}