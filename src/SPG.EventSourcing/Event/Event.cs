using System;

namespace SPG.EventSourcing.Event
{
    public class Event : IEvent
    {
        public int TargetVersion { get; set; }
        public Guid AggregateId { get; set; }
        public Guid CorrelationId { get; }
        public DateTime EventCommittedTimestamp { get; set; }
        public int ClassVersion { get; set; }

        public Event()
        {
        }

        /// <summary>
        /// This method makes it easier to construct a new Event
        /// </summary>
        /// <param name="aggregateId">Aggregate ID</param>
        /// <param name="targetVersion">Target Version</param>
        /// <param name="eventClassVersion">Version of the current class</param>
        public Event(Guid aggregateId, int targetVersion, int eventClassVersion) : base()
        {
            this.AggregateId = aggregateId;
            this.TargetVersion = targetVersion;
            this.ClassVersion = eventClassVersion;
            this.CorrelationId = Guid.NewGuid();
        }
    }
}