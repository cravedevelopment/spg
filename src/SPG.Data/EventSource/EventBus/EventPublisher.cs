using System.Threading.Tasks;
using SPG.EventSourcing.Event;
using SPG.EventSourcing.EventBus;

namespace SPG.Data.EventSource.EventBus
{
    public class EventPublisher : IEventPublisher
    {
        private readonly 
        public Task PublishAsync(IEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}