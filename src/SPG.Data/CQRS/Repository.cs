using System;
using System.Collections.Generic;
using System.Text;
using SPG.Data.CQRS.Commands;

namespace SPG.Data.CQRS
{
    public class Repository : RepositoryBase  //shortcut you can do as you see fit with new()
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public override TResult GetById<TResult>(Guid id)
        {
            var obj = new TResult();//lots of ways to do this
            var e = _storage.GetEventsForAggregate(id);
            obj.LoadsFromHistory(e);
            return obj;
        }

        public override void SaveEvents(AggregateRoot aggregate, int expectedVersion)
        {
            throw new NotImplementedException();
        }

        public override void SaveCommand<TCommand>(TCommand command)
        {
            throw new NotImplementedException();
        }

        public override TResult GetByUserId<TResult>(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
