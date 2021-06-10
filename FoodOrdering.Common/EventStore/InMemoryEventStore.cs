using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Common.EventStore
{
	public class InMemoryEventStore : IEventStore
	{
		private readonly Dictionary<string, IEnumerable<IEvent>> streams = new();

		public void AppendEventsToStream(string streamId, IEnumerable<IEvent> events, int initialVersion)
		{
			var stream = streams[streamId];
			if (stream.Count() != initialVersion)
			{
				throw new AppException("Optimistic locking exception");
			}

			streams[streamId] = stream.Concat(events);
		}

		public void CreateNewStream(string streamId, IEnumerable<IEvent> events)
		{
			streams.Add(streamId, events);
		}

		public IEnumerable<IEvent> GetStream(string streamId)
			=> streams[streamId];
	}
}
