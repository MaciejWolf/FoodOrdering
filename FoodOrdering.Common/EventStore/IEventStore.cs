using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Documents.Session;
using static FoodOrdering.Common.EventStore.EventStream;

namespace FoodOrdering.Common.EventStore
{
	public interface IEventStore
	{
		void CreateNewStream(string streamId, IEnumerable<IEvent> events);
		void AppendEventsToStream(string streamId, IEnumerable<IEvent> events, int initialVersion);
		IEnumerable<IEvent> GetStream(string streamId);
	}

	public class RavenDbEventStore : IEventStore
	{
		private readonly IDocumentSession documentSession;

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
				.Query<EventWrapper>()
				.Customize(x => x.WaitForNonStaleResults())
				.Where(e => e.StreamId == streamId)
				.OrderBy(e => e.EventNumber)
				.Select(e => e.Event)
				.ToArray();

			return events;
		}
	}

	public class EventStream
	{
		public string Id { get; set; }
		public int Version { get; set; }

		public EventWrapper RegisterEvent(IEvent evnt)
		{
			Version++;

			return new EventWrapper
			{
				Event = evnt,
				EventNumber = Version,
				StreamId = Id,
			};
		}

		public class EventWrapper
		{
			public string Id => $"{StreamId}-{EventNumber}";
			public IEvent Event { get; set; }
			public string StreamId { get; set; }
			public int EventNumber { get; set; }
		}
	}
}
