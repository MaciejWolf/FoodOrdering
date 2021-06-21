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
		protected readonly IList<IEvent> allEvents = new List<IEvent>();
		protected readonly IList<IEvent> uncommittedEvents = new List<IEvent>();
		public T Id { get; protected set; }

		public IEnumerable<IEvent> AllEvents => allEvents;

		public IEnumerable<IEvent> UncommittedEvents => uncommittedEvents;

		public int InitialVersion { get; protected set; }

		public int Version { get; protected set; }

		public abstract void ApplyEvent(IEvent evnt);

		public void ClearUncommittedEvents()
		{
			uncommittedEvents.Clear();
		}

		protected void AddEvent(IEvent evnt)
		{
			ApplyEvent(evnt);
			uncommittedEvents.Add(evnt);
		}

	}
}
