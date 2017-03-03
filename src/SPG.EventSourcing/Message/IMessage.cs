using System;

namespace SPG.EventSourcing.Message
{
    public interface IMessage
    {
        Guid CorrelationId { get; }
    }
}