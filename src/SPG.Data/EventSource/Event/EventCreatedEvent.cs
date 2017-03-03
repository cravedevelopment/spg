using System;

namespace SPG.Data.EventSource.Event
{
    public class EventCreatedEvent : EventSourcing.Event.Event
    {
        private static int _currrentTypeVersion = 1;
        public DateTime EventDate { get; set; }
        public int CustomerId { get; set; }
        public int SiteId { get; set; }
        public int EventType { get; set; }
        public int EventState { get; set; }
        public byte[] EventData { get; set; }

        public EventCreatedEvent(Guid aggregateId,int version, DateTime eventDate, int customerId, int siteId, int eventType, int eventState, byte[] eventData)
            :base(aggregateId, version, _currrentTypeVersion)
        {
            this.EventDate = eventDate;
            this.CustomerId = customerId;
            this.SiteId = siteId;
            this.EventType = eventType;
            this.EventState = eventState;
            this.EventData = eventData;
        }
    }
}