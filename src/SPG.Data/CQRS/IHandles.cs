namespace SPG.Data.CQRS.Handlers
{
    public interface IHandles<T>
    {
        void Handle(T message);
    }
}
