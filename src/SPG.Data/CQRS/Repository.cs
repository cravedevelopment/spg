using System;
using System.Collections.Generic;
using System.Text;
using SPG.Data.CQRS.Commands;

namespace SPG.Data.CQRS
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
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

        public T GetById(Guid id)
        {
            var obj = new T();//lots of ways to do this
            var e = _storage.GetEventsForAggregate(id);
            obj.LoadsFromHistory(e);
            return obj;
        }

        public void SaveEvents(AggregateRoot aggregate, int expectedVersion)
        {
            throw new NotImplementedException();
        }

        public void SaveCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
