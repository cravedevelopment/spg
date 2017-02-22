using SPG.Data.CQRS.Commands;
using SPG.Data.CQRS.WriteModel;

namespace SPG.Data.CQRS.CommandHandlers
{
    public class SampleCommandHandler
    {
        private readonly IRepository<SampleModel> _repository;

        public SampleCommandHandler(IRepository<SampleModel> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateSampleCommand message)
        {
            _repository.SaveCommand(message);

            // For clarification if we need to store the event after the command.
            var item = new SampleModel(message.Id, message.CommandDate, message.CustomerId, message.SiteId, message.CommandType, message.CommandState, message.CommandData);
            _repository.SaveEvents(item, -1);

        }
    }
}
