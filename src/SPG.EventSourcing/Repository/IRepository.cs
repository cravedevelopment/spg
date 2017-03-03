using System;
using System.Threading.Tasks;
using SPG.EventSourcing.Domain;

namespace SPG.EventSourcing.Repository
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(Guid id) where T : AggregateRoot;
        Task SaveAsync<T>(T aggregate) where T : AggregateRoot;
    }
}