using System.Threading.Tasks;

namespace webapi.src.Shared.Domain
{
    public interface IQueryBus
    {
        Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}