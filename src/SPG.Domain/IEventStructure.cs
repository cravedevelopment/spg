using System;

namespace POC.Domain
{
    public interface IEventStructure
    {
        Guid Id { get; }
        DateTime EventDate { get; }
        int CustomerId { get; }
        int SiteId { get; }
        int EventType { get; }
        int EventState { get; }
        byte[] EventData { get; }
    }
}
