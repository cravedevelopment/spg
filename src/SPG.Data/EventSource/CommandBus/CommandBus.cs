using System;
using System.Threading.Tasks;
using SPG.Data.EventSource.CommandHandler;
using SPG.EventSourcing.Command;
using SPG.EventSourcing.CommandBus;
using SPG.EventSourcing.CommandHandler;

namespace SPG.Data.EventSource.CommandBus
{
    public class CommandBus : ICommandBus
    {
        private readonly EventCommandHandler _eventCommandHandler;

        public CommandBus(EventCommandHandler eventCommandHandler)
        {
            _eventCommandHandler = eventCommandHandler;
        }

        public async Task<ICommandPublishResult> ExecuteAsync<T>(T command) where T : ICommand
        {
;
            var handler = _eventCommandHandler as ICommandHandler<T>;
            if (handler != null)
            {
                Task.Run(() => handler.HandleCommandAsync(command)).ConfigureAwait(false);

                return new CommandPublishResult(true, "", null);
            }
            else
            {
                return new CommandPublishResult(false, "No command handler",
                    new NullReferenceException($"Command handler not found for {typeof(T)}"));
            }
        }
    }
}