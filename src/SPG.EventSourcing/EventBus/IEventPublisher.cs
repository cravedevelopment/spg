using System.Threading.Tasks;
using SPG.EventSourcing.Event;

namespace SPG.EventSourcing.EventBus
{
    public interface IEventPublisher
    {
        Task PublishAsync(IEvent @event);
    }
}