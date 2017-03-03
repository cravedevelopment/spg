namespace SPG.EventSourcing.Command
{
    public interface ICommandPublishResult
    {
        bool IsSucess { get; }
        string FailReason { get; }
        System.Exception ResultException { get; }
        void EnsurePublished();
    }
}