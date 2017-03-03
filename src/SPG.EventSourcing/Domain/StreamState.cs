namespace SPG.EventSourcing.Domain
{
    public abstract partial class AggregateRoot
    {
        public enum StreamState
        {
            NoStream = -1,
            HasStream = 1
        }
    }
}