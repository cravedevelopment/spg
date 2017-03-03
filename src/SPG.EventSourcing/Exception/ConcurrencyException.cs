namespace SPG.EventSourcing.Exception
{
    public class ConcurrencyException : System.Exception
    {
        public ConcurrencyException(string msg) : base(msg)
        {

        }
    }
}