using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Common.EventStore;
using FoodOrdering.Modules.Basket.Domain.Basket;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;
using MediatR;

namespace FoodOrdering.Modules.Basket.Infrastructure.Repositories
{
	public class BasketsRepository : IBasketsRepository
	{
		private readonly IEventStore eventStore;
		private readonly IPublisher publisher;

		public BasketsRepository(IEventStore eventStore, IPublisher publisher)
		{
			this.eventStore = eventStore;
			this.publisher = publisher;
		}

		public IEnumerable<BasketAggregate> GetAll()
		{
			throw new NotImplementedException();
		}

		public BasketAggregate GetById(ClientId clientId)
		{
			var streamId = GetStreamId(clientId);
			var events = eventStore.GetStream(streamId);
			var basket = BasketAggregate.FromEvents(events);
			return basket;
		}

		public void Save(BasketAggregate basket)
		{
			var streamId = GetStreamId(basket.Id);

			eventStore.CreateNewStream(streamId, basket.UncommittedEvents);

			PublishEvents(basket.UncommittedEvents);
		}

		public void Update(BasketAggregate basket)
		{
			var streamId = GetStreamId(basket.Id);

			eventStore.AppendEventsToStream(streamId, basket.UncommittedEvents, basket.InitialVersion);

			PublishEvents(basket.UncommittedEvents);
		}

		private async Task PublishEvents(IEnumerable<IEvent> events)
		{
			foreach (var e in events)
			{
				await publisher.Publish(e);
			}
		}

		private static string GetStreamId(ClientId clientId)
			=> $"Basket:{clientId}";
	}
}
