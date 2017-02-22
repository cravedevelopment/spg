using SPG.Data.CQRS.Messages;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SPG.Data.CQRS.Events
{
    public class EventBus : IEventPublisher
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _routes = new Dictionary<Type, List<Action<IMessage>>>();
        public void Publish<T>(T @event) where T : IEvent
        {
            List<Action<IMessage>> handlers;

            if (!_routes.TryGetValue(@event.GetType(), out handlers)) return;

            foreach (var handler in handlers)
            {
                //dispatch on thread pool for added awesomeness
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1(@event));
            }
        }
    }
}
