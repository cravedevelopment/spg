using System.Threading.Tasks;
using SPG.EventSourcing.Command;

namespace SPG.EventSourcing.CommandBus
{
    public interface ICommandBus
    {
        Task<ICommandPublishResult> ExecuteAsync<T>(T command) where T : ICommand;
    }
}