using MediatR;

namespace FoodOrdering.Common
{
	public abstract class EventSourcedAggregateRoot<T> : AggregateRoot<T>
    {
        protected abstract void Apply(INotification @event);
    }
}
