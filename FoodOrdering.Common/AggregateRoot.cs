using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Common
{
	public abstract class AggregateRoot<T> : IEntity<T>
	{
		private readonly List<INotification> events = new();
		private bool versionIncremented;

		public T Id { get; protected set; }
		public int Version { get; protected set; }
		public IEnumerable<INotification> Events => events;

        protected void AddEvent(INotification @event)
        {
            if (!events.Any() && !versionIncremented)
            {
                Version++;
                versionIncremented = true;
            }

            events.Add(@event);
        }

        public void ClearEvents() => events.Clear();

        protected void IncrementVersion()
        {
            if (versionIncremented)
            {
                return;
            }

            Version++;
            versionIncremented = true;
        }
    }
}
