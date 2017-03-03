using System;
using System.Collections.Generic;
using System.Text;
using SPG.Data.CQRS.Commands;
using SPG.Data.CQRS.Events;

namespace SPG.Data.CQRS
{
    public abstract class RepositoryBase : IRepository
    {
        public abstract TResult GetById<TResult>(Guid id) where TResult : AggregateRoot, new();
        public abstract void SaveCommand<TCommand>(TCommand command) where TCommand : ICommand;
        public abstract void SaveEvents(AggregateRoot aggregate, int expectedVersion);
        public abstract TResult GetByUserId<TResult>(int userId) where TResult : AggregateRoot, new();
        
        protected int CalculateExpectedVersion<T>(AggregateRoot aggregate, List<T> events)
        {
            var expectedVersion = aggregate.Version - events.Count;
            return expectedVersion;
        }

        protected TResult BuildAggregate<TResult>(IEnumerable<IEvent> events) where TResult : AggregateRoot, new()
        {
            var result = new TResult();
            foreach (var @event in events)
            {
                result.ApplyEvent(@event);
            }
            return result;
        }
    }
}
