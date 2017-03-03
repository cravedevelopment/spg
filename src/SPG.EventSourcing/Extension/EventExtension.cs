using System;
using SPG.EventSourcing.Domain;
using SPG.EventSourcing.Event;
using SPG.EventSourcing.Exception;

namespace SPG.EventSourcing.Extension
{
    public static class EventExtension
    {
        public static void InvokeOnAggregate(this IEvent @event, AggregateRoot aggregate, string methodName)
        {
            var method = ReflectionHelper.GetMethod(aggregate.GetType(), methodName, new Type[] { @event.GetType() }); //Find the right method

            if (method != null)
            {
                method.Invoke(aggregate, new object[] { @event }); //invoke with the event as argument

                // or we can use dynamics
                //dynamic d = this;
                //dynamic e = @event;
                //d.Apply(e);
            }
            else
            {
                throw new AggregateEventOnApplyMethodMissingException($"No event Apply method found on {aggregate.GetType()} for {@event.GetType()}");
            }
        }
    }
}