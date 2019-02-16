using MediatR;

namespace webapi.src.Shared.Domain.Query
{
    public interface IQueryHandler<in TQuery, TResponse>: IRequestHandler<TQuery, TResponse> where TQuery: IQuery<TResponse>
    {
        
    }
}