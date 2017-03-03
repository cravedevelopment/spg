using SPG.Data.CQRS.ReadModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.Data.CQRS.Queries
{
    public class GetEventsByCustomerIdQuery : IQuery<EventReadModel[]>
    {
        public int CustomerId { get; set; }
    }
}
