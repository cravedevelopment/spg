using System;
using System.Collections.Generic;
using System.Linq;
using SPG.EventSourcing.Event;
using SPG.EventSourcing.Exception;
using SPG.EventSourcing.Extension;

namespace SPG.EventSourcing.Domain
{
    public abstract partial class AggregateRoot
    {
        private readonly List<IEvent> _uncommittedChanges;
        private Dictionary<Type, string> _eventHandlerCache;

        /// <summary>
        /// Aggregates unique Guid
        /// </summary>
        public Guid Id { get; protected set; }
        /// <summary>
        /// Current version of the Aggregate. Starts with -1 and parameterized constructor increments it by 1.
        /// All events will increment this by 1 when Applied.
        /// </summary>
        public int CurrentVersion { get; protected set; }
        /// <summary>
        /// This is the CurrentVersion of the Aggregate when it was saved last. This is used to ensure optimistic concurrency. 
        /// </summary>
        public int LastCommittedVersion { get; protected set; }

        /// <summary>
        /// GetAsync the current state of this Aggregate
        /// </summary>
        /// <returns>StreamState</returns>
        public StreamState GetStreamState()
        {
            return CurrentVersion == -1 ? StreamState.NoStream : StreamState.HasStream;
        }

        protected AggregateRoot()
        {
            CurrentVersion = (int)StreamState.NoStream;
            LastCommittedVersion = (int)StreamState.NoStream;
            _uncommittedChanges = new List<IEvent>();
            SetupInternalEventHandlers();
        }
        
        public bool HasUncommittedChanges()
        {
            lock (_uncommittedChanges)
            {
                return _uncommittedChanges.Any();
            }
        }
        
        public IEnumerable<IEvent> GetUncommittedChanges()
        {
            lock (_uncommittedChanges)
            {
                return _uncommittedChanges.ToList();
            }
        }
        
        public void MarkChangesAsCommitted()
        {
            lock (_uncommittedChanges)
            {
                _uncommittedChanges.Clear();
                LastCommittedVersion = CurrentVersion;
            }
        }

      
        public void LoadsFromHistory(IEnumerable<IEvent> history)
        {
            foreach (var e in history)
            {
                //We call ApplyEvent with isNew parameter set to false as we are replaying a historical event
                ApplyEvent(e, false);
            }
            LastCommittedVersion = CurrentVersion;
        }

        protected void ApplyEvent(IEvent @event)
        {
            ApplyEvent(@event, true);
        }

        private void ApplyEvent(IEvent @event, bool isNew)
        {
            //All state changes to AggregateRoot must happen via the Apply method
            //Make sure the right Apply method is called with the right type.
            //We can you use dynamic objects or reflection for this.

            if (CanApply(@event))
            {
                DoApply(@event);

                if (!isNew) return;
                lock (_uncommittedChanges)
                {
                    _uncommittedChanges.Add(@event); //only add to the events collection if it's a new event
                }
            }
            else
            {
                throw new AggregateStateMismatchException(
                    $"The event target version is {@event.AggregateId}.(Version {@event.TargetVersion}) and " +
                    $"AggregateRoot version is {this.Id}.(Version {CurrentVersion})");
            }

        }

        private bool CanApply(IEvent @event)
        {
            //Check to see if event is applying against the right Aggregate and matches the target version
            return ((GetStreamState() == StreamState.NoStream) || (Id == @event.AggregateId)) && (CurrentVersion == @event.TargetVersion);
        }

   
        private void DoApply(IEvent @event)
        {
            if (GetStreamState() == StreamState.NoStream)
            {
                Id = @event.AggregateId; //This is only needed for the very first event as every other event CAN ONLY apply to matching ID
            }

            if (_eventHandlerCache.ContainsKey(@event.GetType()))
            {
                @event.InvokeOnAggregate(this, _eventHandlerCache[@event.GetType()]);
            }
            else
            {
                throw new AggregateEventOnApplyMethodMissingException($"No event handler specified for {@event.GetType()} on {this.GetType()}");
            }

            CurrentVersion++;
        }

        /// <summary>
        /// This will wireup the event handling methods to corresponding events
        /// </summary>
        private void SetupInternalEventHandlers()
        {
            _eventHandlerCache = ReflectionHelper.FindEventHandlerMethodsInAggregate(this.GetType());
        }
    }
}