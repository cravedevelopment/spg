namespace SPG.Data.CQRS
{
    public interface IHandles<T>
    {
        void Handle(T message);
    }
}
