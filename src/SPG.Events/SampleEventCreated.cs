using SPG.Data.CQRS;
using SPG.Data.CQRS.Domain;
using System;

namespace SPG.Events
{
    public class SampleEventCreated : BaseEvent
    {
       
        public SampleEventCreated(Guid id, DateTime eventDate, int customerId, int siteId, int eventType, 
            int eventState, byte[] eventData)
        {
            Id = id;
            EventDate = eventDate;
            CustomerId = customerId;
            SiteId = siteId;
            EventType = eventType;
            EventState = eventState;
            EventData = eventData;
        }
    }
}
