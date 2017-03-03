using System;
using SPG.Data.EventSource.Event;
using SPG.Data.EventSource.Snapshot;
using SPG.EventSourcing.Domain;
using SPG.EventSourcing.Snapshot;

namespace SPG.Data.EventSource.Domain
{
    public class Event : AggregateRoot, ISnapshottable
    {
        public DateTime CreatedDate { get; private set; }
        public DateTime EventDate { get; private set; }
        public int CustomerId { get; private set; }
        public int SiteId { get; private set; }
        public int EventType { get; private set; }
        public int EventState { get; private set; }
        public byte[] EventData { get; private set; }

        public Event()
        {

        }
        public Event(Guid id, DateTime eventDate, int customerId, int siteId, int eventType, int eventState, byte[] eventData) : this()
        {
            ApplyEvent(new EventCreatedEvent(id, CurrentVersion, eventDate, customerId, siteId, eventType, eventState,
                eventData));
        }


        #region "Snapshots" 
        EventSourcing.Snapshot.Snapshot ISnapshottable.TakeSnapshot()
        {
            return new EventSnapshot(Guid.NewGuid(), Id, CurrentVersion, CreatedDate, EventDate, CustomerId, SiteId, EventType, EventState, EventData);
        }

        public void ApplySnapshot(EventSourcing.Snapshot.Snapshot snapshot)
        {
            var item = (EventSnapshot)snapshot;
            Id = item.AggregateId;
            CurrentVersion = item.Version;
            LastCommittedVersion = item.Version;
            CreatedDate = item.CreatedDate;
            CustomerId = item.CustomerId;
            SiteId = item.SiteId;
            EventType = item.EventType;
            EventState = item.EventState;
            EventData = item.EventData;
            EventDate = item.EventDate;
        }
        #endregion  
    }
}