using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents.Session;
using static FoodOrdering.Common.EventStore.EventStream;

namespace FoodOrdering.Common.EventStore.RavenDb
{
	public class RavenDbEventStore : IEventStore
	{
		private readonly IDocumentSession documentSession;

		public RavenDbEventStore()
		{
		}

		public RavenDbEventStore(IDocumentSession documentSession)
		{
			this.documentSession = documentSession;
		}

		public void AppendEventsToStream(string streamId, IEnumerable<IEvent> events, int initialVersion)
		{
			var stream = documentSession.Load<EventStream>(streamId);

			if (stream.Version != initialVersion)
			{
				throw new Exception("Optimistic locking");
			}

			foreach (var e in events)
			{
				documentSession.Store(stream.RegisterEvent(e));
			}
		}

		public void CreateNewStream(string streamId, IEnumerable<IEvent> events)
		{
			var eventStream = new EventStream { Id = streamId };
			documentSession.Store(eventStream);

			AppendEventsToStream(streamId, events, 0);
			documentSession.SaveChanges();
		}

		public IEnumerable<IEvent> GetStream(string streamId)
		{
			var events = documentSession
				.Query<EventMetadata>()
				.Customize(x => x.WaitForNonStaleResults())
				.Where(e => e.StreamId == streamId)
				.OrderBy(e => e.EventNumber)
				.Select(e => e.Event)
				.ToArray();

			return events;
		}
	}
}
