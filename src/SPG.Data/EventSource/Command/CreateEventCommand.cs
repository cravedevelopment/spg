using System;

namespace SPG.Data.EventSource.Command
{
    public class CreateEventCommand : EventSourcing.Command.Command
    {
        public DateTime EventDate { get; private set; }
        public int CustomerId { get; private set; }
        public int SiteId { get; private set; }
        public int EventType { get; private set; }
        public int EventState { get; private set; }
        public byte[] EventData { get; private set; }

        public CreateEventCommand(Guid correlationId, Guid aggregateId, int targetVersion, DateTime eventDate, int eventType, int eventState, byte[] eventData, int customerId, int siteId) 
            : base(correlationId, aggregateId, targetVersion)
        {
            EventDate = eventDate;
            EventType = eventType;
            EventState = eventState;
            EventData = eventData;
            CustomerId = customerId;
            SiteId = siteId;
        }
    }
}