using System;

namespace SPG.Data.EventSource.Snapshot
{
    public class EventSnapshot : EventSourcing.Snapshot.Snapshot
    {
        public DateTime CreatedDate { get; private set; }
        public DateTime EventDate { get; private set; }
        public int CustomerId { get; private set; }
        public int SiteId { get; private set; }
        public int EventType { get; private set; }
        public int EventState { get; private set; }
        public byte[] EventData { get; private set; }


        public EventSnapshot(Guid id, Guid aggregateId, int version, DateTime createdDate, DateTime eventDate, int customerId, int siteId, int eventType, int eventState, byte[] eventData)
            : base(id, aggregateId, version)
        {
            CreatedDate = createdDate;
            EventDate = eventDate;
            CustomerId = customerId;
            SiteId = siteId;
            EventType = eventType;
            EventState = eventState;
            EventData = eventData;
        }
    }
}