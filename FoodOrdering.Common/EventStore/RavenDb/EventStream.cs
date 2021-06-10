namespace FoodOrdering.Common.EventStore
{
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
