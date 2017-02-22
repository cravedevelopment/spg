using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using SPG.Data.CQRS.Commands;

namespace SPG.Data.CQRS
{
    public class EventStoreRepository<T> : IRepository<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
    {
        private IEventStoreConnection _connection;
        const string eventStreamName = "spg_event_stream";
        const string commandStreamName = "spg_command_stream";
        public EventStoreRepository(IEventStoreConnection connection)
        {
            _connection = connection;
        }
        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveEvents(AggregateRoot aggregate, int expectedVersion)
        {
            var events = aggregate.GetUncommittedChanges();
            var eventData = events.Select(CreateEventData);
            _connection.AppendToStreamAsync(eventStreamName, ExpectedVersion.Any, eventData);
        }
        
        public EventData CreateEventData(object @event)
        {
          
            var data = SerializeObject(@event);
            var eventData = new EventData(Guid.NewGuid(), @event.GetType().Name, true, data, null);
            return eventData;
        }
        private byte[] SerializeObject(object obj)
        {
            var jsonObj = JsonConvert.SerializeObject(obj);
            var data = Encoding.UTF8.GetBytes(jsonObj);
            return data;
        }

        public void SaveCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandData = CreateEventData(command);
            _connection.AppendToStreamAsync(commandStreamName, ExpectedVersion.Any, commandData);
        }

   
    }
}
