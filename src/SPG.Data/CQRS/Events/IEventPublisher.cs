using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.Data.CQRS.Events
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}
