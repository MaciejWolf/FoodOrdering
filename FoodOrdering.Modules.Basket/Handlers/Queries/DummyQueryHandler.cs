using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Basket.Contracts.Queries;
using FoodOrdering.Modules.Basket.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Queries
{
	class DummyQueryHandler : IRequestHandler<DummyQuery, string>
	{
		private readonly ISender sender;
		private readonly IPublisher publisher;
		private readonly IBasketsRepository basketsRepository;
		private readonly IClock clock;

		public DummyQueryHandler(ISender sender, IPublisher publisher, IBasketsRepository basketsRepository, IClock clock)
		{
			this.sender = sender;
			this.publisher = publisher;
			this.basketsRepository = basketsRepository;
			this.clock = clock;
		}

		public async Task<string> Handle(DummyQuery request, CancellationToken cancellationToken)
		{
			return "Great!";
		}
	}
}
