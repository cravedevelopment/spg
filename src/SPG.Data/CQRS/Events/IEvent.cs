using SPG.Data.CQRS.Messages;
using System;

namespace SPG.Data.CQRS
{
    public interface IEvent : IMessage
    {
            
        int Version { get; set; }
      
    }
}
