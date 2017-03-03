using SPG.Data.CQRS.Domain;
using SPG.Data.CQRS.Queries;
using SPG.Data.CQRS.ReadModel;
using SPG.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.Data.CQRS.QueryHandlers
{
    public class GetEventsByCustomerIdQueryHandler : IQueryHandler<GetEventsByCustomerIdQuery, EventReadModel[]>
    {
        private readonly IRepository _repository;
        public GetEventsByCustomerIdQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public EventReadModel[] Handle(GetEventsByCustomerIdQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
