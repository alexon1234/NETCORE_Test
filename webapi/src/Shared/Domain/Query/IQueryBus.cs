using System.Threading.Tasks;

namespace webapi.src.Shared.Domain.Query
{
    public interface IQueryBus
    {
        Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery: IQuery<TResponse>;
    }
}