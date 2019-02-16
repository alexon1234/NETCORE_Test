using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace webapi.src.Shared.Domain.Command
{
    public abstract class ICommandHandler<T> : AsyncRequestHandler<T> where T : ICommand
    {
    }
}