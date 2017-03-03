namespace SPG.EventSourcing.Exception
{
    public class AggregateNotFoundException : System.Exception
    {
        public AggregateNotFoundException(string msg) : base(msg)
        {

        }
    }
}