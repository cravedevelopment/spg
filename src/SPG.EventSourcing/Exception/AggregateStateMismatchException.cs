namespace SPG.EventSourcing.Exception
{
    public class AggregateStateMismatchException : System.Exception
    {
        public AggregateStateMismatchException(string msg) : base(msg)
        {

        }
    }
}