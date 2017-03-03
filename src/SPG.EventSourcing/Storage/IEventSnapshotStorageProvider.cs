using System;
using System.Threading.Tasks;

namespace SPG.EventSourcing.Storage
{
    public interface IEventSnapshotStorageProvider : ISnapshotStorageProvider
    {
        Task<Snapshot.Snapshot> GetSnapshotAsync(Type aggregateType, Guid aggregateId, int version);
    }
}