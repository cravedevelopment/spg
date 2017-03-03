using System.Threading.Tasks;
using SPG.EventSourcing.Logger;
using SPG.EventSourcing.Repository;

namespace SPG.Data.EventSource.Repository
{
    public class EventRepository: RepositoryDecorator
    {
        public EventRepository(IRepository repository) : base(repository)
        {
        }

        public override async Task SaveAsync<TAggregate>(TAggregate aggregate)
        {
            LogManager.Log($"Saving {aggregate.GetType().Name}...", LogSeverity.Debug);
            await base.SaveAsync(aggregate);
            LogManager.Log($"{aggregate.GetType().Name} Saved...", LogSeverity.Debug);
        }
    }
}