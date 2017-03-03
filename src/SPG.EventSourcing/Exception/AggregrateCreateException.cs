namespace SPG.EventSourcing.Exception
{
    public class AggregateCreationException : System.Exception
    {
        public AggregateCreationException(string msg) : base(msg)
        {

        }
    }
}