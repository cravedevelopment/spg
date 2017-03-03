using System;

namespace SPG.EventSourcing.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class InternalEventHandler : Attribute
    {
    }
}