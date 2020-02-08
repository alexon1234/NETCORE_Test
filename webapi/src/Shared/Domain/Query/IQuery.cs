using MediatR;

namespace webapi.src.Shared.Domain
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {

    }
}