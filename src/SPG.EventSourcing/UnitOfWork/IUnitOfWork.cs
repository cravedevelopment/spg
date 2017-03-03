using System;
using System.Threading.Tasks;
using SPG.EventSourcing.Domain;

namespace SPG.EventSourcing.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<T> GetAsync<T>(Guid id, int? expectedVersion = null) where T : AggregateRoot;

        void Add<T>(T aggregate) where T : AggregateRoot;

        Task CommitAsync();
    }
}