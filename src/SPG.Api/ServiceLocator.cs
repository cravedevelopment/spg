using SPG.Data.CQRS.Commands;

namespace SPG.WebAPI
{
    public static class ServiceLocator
    {
        public static CommandBus Bus { get; set; }

    }
}