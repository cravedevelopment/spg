using SPG.Data.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.Data.CQRS.Commands
{

    public class CreateSampleCommand : BaseCommand
    {
        public CreateSampleCommand(Guid id, DateTime commandDate, int customerId, int siteId, int commandType, int commandState, byte[] commandData)
        {
            Id = id;
            CommandDate = commandDate;
            CustomerId = customerId;
            SiteId = siteId;
            CommandType = commandType;
            CommandState = commandState;
            CommandData = commandData;
            
        }
    }
}
