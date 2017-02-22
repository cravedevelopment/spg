using SPG.Data.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPG.Data.CQRS.Commands
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : ICommand;
    }
}
