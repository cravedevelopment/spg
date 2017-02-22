using SPG.Data.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.Data.CQRS
{

    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void SaveEvents(AggregateRoot aggregate, int expectedVersion);
        void SaveCommand<TCommand>(TCommand command) where TCommand : ICommand;
        T GetById(Guid id);
    }
}
