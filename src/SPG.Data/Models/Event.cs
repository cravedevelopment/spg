//using SPG.Data.CQRS;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SPG.Data.Models
//{
//    public class Event : AggregateRoot
//    {
//        private Guid id;

//        public Guid GetId()
//        {
//            return id;
//        }

//        public void SetId(Guid value)
//        {
//            id = value;
//        }

//        public DateTime EventDate { get; set; }
//        public int CustomerId { get; set; }
//        public int SiteId { get; set; }
//        public int EventType { get; set; }
//        public int EventState { get; set; }
//        public byte[] EventData { get; set; }

//        public override Guid Id => throw new NotImplementedException();
//    }
//}
