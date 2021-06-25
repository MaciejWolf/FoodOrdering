namespace FoodOrdering.Common.EventStore
{
	public class EventStream
	{
		public string Id { get; set; }
		public int Version { get; set; }

		public EventMetadata RegisterEvent(IEvent evnt)
		{
			Version++;

			return new EventMetadata
			{
				Event = evnt,
				EventNumber = Version,
				StreamId = Id,
			};
		}

		public class EventMetadata
		{
			public string Id => $"{StreamId}-{EventNumber}";
			public IEvent Event { get; set; }
			public string StreamId { get; set; }
			public int EventNumber { get; set; }
		}
	}
}
