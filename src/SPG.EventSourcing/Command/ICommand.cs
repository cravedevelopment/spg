using System;
using SPG.EventSourcing.Message;

namespace SPG.EventSourcing.Command
{
    public interface ICommand : IMessage
    {
        Guid AggregateId { get; }
    }
}