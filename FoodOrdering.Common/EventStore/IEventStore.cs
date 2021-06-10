using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Common.EventStore
{
	public interface IEventStore
	{
		void CreateNewStream(string streamId, IEnumerable<IEvent> events);
		void AppendEventsToStream(string streamId, IEnumerable<IEvent> events, int initialVersion);
		IEnumerable<IEvent> GetStream(string streamId);
	}
}
