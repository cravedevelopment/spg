using SPG.Data.CQRS.Commands;
using System;

namespace SPG.Data.CQRS.Domain
{
    public class BaseCommand : ICommand
    {
        public Guid Id { get; set; }
        public DateTime CommandDate { get; set; }
        public int CustomerId { get; set; }
        public int SiteId { get; set; }
        public int CommandType { get; set; }
        public int CommandState { get; set; }
        public byte[] CommandData { get; set; }
        public int ExpectedVersion { get; set; }
    }
}
