namespace SPG.Data.CQRS.Events
{
    public interface IEvent : IMessage
    {
            
        int Version { get; set; }
      
    }
}
