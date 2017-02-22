using SPG.Data.CQRS.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.Data.CQRS.Commands
{
    public interface ICommand : IMessage
    {
        int ExpectedVersion { get; set; }
    }
}
