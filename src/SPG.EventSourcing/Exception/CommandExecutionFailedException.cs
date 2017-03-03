namespace SPG.EventSourcing.Exception
{
    public class CommandExecutionFailedException : System.Exception
    {
        public CommandExecutionFailedException(string msg) : base(msg)
        {

        }
    }
}