using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace webapi.src.Shared.Domain
{
    public abstract class ICommandHandler<T> : AsyncRequestHandler<T> where T : ICommand
    {
    }
}