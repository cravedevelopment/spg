using System;
using System.Collections.Generic;
using System.Text;
using SPG.Data.CQRS.Events;

namespace SPG.Data.CQRS
{
    public abstract class AggregateRoot
    {
        private readonly List<IEvent> _changes = new List<IEvent>();
        private readonly Dictionary<Type, Action<IEvent>> _routes = new Dictionary<Type, Action<IEvent>>();
        public abstract Guid Id { get; }
        public int Version { get; internal set; }



        protected void RegisterTransition<T>(Action<T> transition) where T : class
        {
            _routes.Add(typeof(T), o => transition(o as T));
        }

        public IEnumerable<IEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<IEvent> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(IEvent @event)
        {
            ApplyChange(@event, true);
        }
        public void ApplyEvent(IEvent @event)
        {
            var eventType = @event.GetType();
            if (_routes.ContainsKey(eventType))
            {
                _routes[eventType](@event);
            }
            Version++;
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyChange(IEvent @event, bool isNew)
        {
            ApplyEvent(@event);
            _changes.Add(@event);
        }
    }
}
