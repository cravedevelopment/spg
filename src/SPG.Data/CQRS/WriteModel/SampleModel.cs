using SPG.Data.CQRS.Events;
using System;

namespace SPG.Data.CQRS.WriteModel
{
    public class SampleModel : AggregateRoot
    {
        private bool _activated;
        private Guid _id;
        internal int Price { get; private set; }

        public SampleModel()
        {
            RegisterTransition<SampleEventCreated>(Apply);
        }
        private void Apply(SampleEventCreated e)
        {
            _id = e.Id;
            _activated = true;
        }
        public override Guid Id => _id;
        public SampleModel(Guid id, DateTime eventDate, int customerId, int siteId, int eventType,
            int eventState, byte[] eventData)
        {
            ApplyChange(new SampleEventCreated(id, eventDate, customerId, siteId, eventType,
             eventState, eventData));
        }
       
     
    }
}
