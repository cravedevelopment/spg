namespace SPG.EventSourcing.Snapshot
{
    public interface ISnapshottable
    {
        Snapshot TakeSnapshot();
        void ApplySnapshot(Snapshot snapshot);
    }
}