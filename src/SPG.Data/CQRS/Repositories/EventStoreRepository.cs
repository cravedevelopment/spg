using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using SPG.Data.CQRS.Events;

namespace SPG.Data.CQRS.Repositories
{
    public class EventStoreRepository: RepositoryBase 
    {
        private readonly IEventStoreConnection _connection;
        private const string EventStreamName = "spg_event_stream";
        private const string CommandStreamName = "spg_command_stream";
        public EventStoreRepository(IEventStoreConnection connection)
        {
            _connection = connection;
        }
        public override TResult GetById<TResult>(Guid id)
        {
            var eventsSlice = _connection.ReadStreamEventsForwardAsync(EventStreamName, 0, int.MaxValue, false).Result;
            //if (eventsSlice.Status.Equals(SliceReadStatus.StreamNotFound))
            //{
            //    throw new AggregateNotFoundException("Could not found aggregate of type " + typeof(TResult) + " and id " + id);
            //}
            var deserializedEvents = eventsSlice.Events.Select(e =>
            {
                var metadata = DeserializeObject<Dictionary<string, string>>(e.OriginalEvent.Metadata);
                var eventData = DeserializeObject(e.OriginalEvent.Data, metadata[EventClrTypeHeader]);
                return eventData as IEvent;
            });
            return BuildAggregate<TResult>(deserializedEvents);
        }

        public override void SaveEvents(AggregateRoot aggregate, int expectedVersion)
        {
            var events = aggregate.GetUncommittedChanges();
            var eventData = events.Select(CreateEventData);
            _connection.AppendToStreamAsync(EventStreamName, ExpectedVersion.Any, eventData);
        }
        
        public EventData CreateEventData(object @event)
        {
            var data = SerializeObject(@event);
            var eventData = new EventData(Guid.NewGuid(), @event.GetType().Name, true, data, null);
            return eventData;
        }
        private T DeserializeObject<T>(byte[] data)
        {
            return (T)(DeserializeObject(data, typeof(T).AssemblyQualifiedName));
        }

        private object DeserializeObject(byte[] data, string typeName)
        {
            var jsonString = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject(jsonString, Type.GetType(typeName));
        }
        private byte[] SerializeObject(object obj)
        {
            var jsonObj = JsonConvert.SerializeObject(obj);
            var data = Encoding.UTF8.GetBytes(jsonObj);
            return data;
        }

        public override void SaveCommand<TCommand>(TCommand command) 
        {
            var commandData = CreateEventData(command);
            _connection.AppendToStreamAsync(CommandStreamName, ExpectedVersion.Any, commandData);
        }

        public string EventClrTypeHeader = "EventClrTypeName";
        public override TResult GetByUserId<TResult>(int userId)
        {
            var eventsSlice = _connection.ReadStreamEventsForwardAsync(EventStreamName, 0, int.MaxValue, false).Result;
            //if (eventsSlice.Status.Equals(SliceReadStatus.StreamNotFound))
            //{
            //    throw new AggregateNotFoundException("Could not found aggregate of type " + typeof(TResult) + " and id " + id);
            //}
            var deserializedEvents = eventsSlice.Events.Select(e =>
            {
                var metadata = DeserializeObject<Dictionary<string, string>>(e.OriginalEvent.Metadata);
                var eventData = DeserializeObject(e.OriginalEvent.Data, metadata[EventClrTypeHeader]);
                return eventData as IEvent;
            });
            return BuildAggregate<TResult>(deserializedEvents);
        }
    }
}
