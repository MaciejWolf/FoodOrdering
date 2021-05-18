using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common.Time;
using FoodOrdering.Modules.Basket.Contracts.Commands;
using FoodOrdering.Modules.Basket.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Commands
{
	class RemoveExpiredCouponsCommandHandler : IRequestHandler<RemoveExpiredCouponsCommand>
	{
		private readonly IBasketsRepository basketsRepository;
		private readonly IClock clock;

		public RemoveExpiredCouponsCommandHandler(IBasketsRepository basketsRepository, IClock clock)
		{
			this.basketsRepository = basketsRepository;
			this.clock = clock;
		}

		public async Task<Unit> Handle(RemoveExpiredCouponsCommand request, CancellationToken cancellationToken)
		{
			foreach (var basket in basketsRepository.GetAll())
			{
				basket.RemoveExpiredCoupons(clock.Now);
			}

			return Unit.Value;
		}
	}
}
