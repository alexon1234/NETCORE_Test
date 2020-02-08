using System;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using webapi.src.Shared.Domain;

namespace webapi.src.Shared.Infrastructure
{
    public class AggregateRepository<T> where T : AggregateBase, new()
    {
        private readonly IDocumentStore store;

        public AggregateRepository(IDocumentStore store)
        {
            this.store = store;
        }

        public async Task Store(T aggregate)
        {
            using (var session = store.OpenSession())
            {
                // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
                var events = aggregate.GetUncommittedEvents().ToArray();
                session.Events.Append(aggregate.Id, aggregate.Version, events);
                await session.SaveChangesAsync();
            }
            // Once successfully persisted, clear events from list of uncommitted events
            aggregate.ClearUncommittedEvents();
        }

        public async Task<T> Load(Guid id, int? version = null)
        {
            using (var session = store.LightweightSession())
            {
                var aggregate = await session.Events.AggregateStreamAsync<T>(id, version ?? 0);
                return aggregate ?? throw new InvalidOperationException($"No aggregate by id {id}.");
            }
        }
    }


}