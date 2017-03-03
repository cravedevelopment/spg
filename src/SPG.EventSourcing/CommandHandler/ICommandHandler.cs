using System.Threading.Tasks;
using SPG.EventSourcing.Command;

namespace SPG.EventSourcing.CommandHandler
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleCommandAsync(T command);
    }
}