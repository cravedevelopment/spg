﻿using System;

namespace SPG.EventSourcing.Snapshot
{
    public class Snapshot
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }

        public Snapshot()
        {

        }

        public Snapshot(Guid id, Guid aggregateId, int version) : base()
        {
            Id = id;
            AggregateId = aggregateId;
            Version = version;
        }
    }
}