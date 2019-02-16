using MediatR;

namespace webapi.src.Shared.Domain.Query
{
    public interface IQuery<out TResponse>: IRequest<TResponse>
    {
        
    }
}