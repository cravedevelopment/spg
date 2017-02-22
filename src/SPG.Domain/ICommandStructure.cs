using System;

namespace POC.Domain
{
    public interface ICommandStructure
    {
        Guid Id { get; }
        DateTime CommandDate { get; }
        int CustomerId { get; }
        int SiteId { get; }
        int CommandType { get; }
        int CommandState { get; }
        byte[] CommandData { get; }
    }
}
