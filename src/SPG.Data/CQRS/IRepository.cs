using SPG.Data.CQRS.Commands;
using System;

namespace SPG.Data.CQRS
{

    public interface IRepository
    {
        void SaveEvents(AggregateRoot aggregate, int expectedVersion);
        void SaveCommand<TCommand>(TCommand command) where TCommand : ICommand;
        TResult GetById<TResult>(Guid id) where TResult : AggregateRoot, new();
        TResult GetByUserId<TResult>(int userId) where TResult : AggregateRoot, new();
    }
}
