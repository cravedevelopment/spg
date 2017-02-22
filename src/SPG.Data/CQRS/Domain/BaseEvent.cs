using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.Data.CQRS.Domain
{
   public class BaseEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime EventDate { get; set; }
        public int CustomerId { get; set; }
        public int SiteId { get; set; }
        public int EventType { get; set; }
        public int EventState { get; set; }
        public byte[] EventData { get; set; }
        public int Version { get; set; }
    }
}
