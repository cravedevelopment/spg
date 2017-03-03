using System.Threading.Tasks;
using SPG.EventSourcing.Event;

namespace SPG.EventSourcing.EventHandler
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleEventAsync(T @event);
    }
}