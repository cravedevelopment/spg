using System;
using System.Collections.Generic;
using SPG.Data.CQRS.Events;

namespace SPG.Data.CQRS
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion);
        List<IEvent> GetEventsForAggregate(Guid aggregateId);
    }
}
