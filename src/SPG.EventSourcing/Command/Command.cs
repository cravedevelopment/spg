using System;

namespace SPG.EventSourcing.Command
{
    public class Command : ICommand
    {
        public Guid CorrelationId { get; private set; }
        public Guid AggregateId { get; }
        public int TargetVersion { get; private set; }

        public Command(Guid correlationId, Guid aggregateId, int targetVersion)
        {
            this.CorrelationId = correlationId;
            AggregateId = aggregateId;
            TargetVersion = targetVersion;
        }
    }
}