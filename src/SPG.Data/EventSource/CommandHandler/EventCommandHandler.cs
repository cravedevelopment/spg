using System.Threading.Tasks;
using SPG.Data.EventSource.Command;
using SPG.Data.EventSource.Repository;
using SPG.EventSourcing.CommandHandler;
using SPG.EventSourcing.UnitOfWork;

namespace SPG.Data.EventSource.CommandHandler
{
    public class EventCommandHandler : ICommandHandler<CreateEventCommand>
    {
        private readonly EventRepository _repository;

        public EventCommandHandler(EventRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleCommandAsync(CreateEventCommand command)
        {
            var work = new UnitOfWork(_repository);
            var newNote = new Domain.Event(command.AggregateId, command.EventDate, command.CustomerId, command.SiteId, command.EventType, command.EventState, command.EventData);
            work.Add(newNote);

            await work.CommitAsync();
        }
    }
}