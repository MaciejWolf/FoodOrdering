using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Coupons.Contracts.Commands;
using FoodOrdering.Modules.Coupons.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Coupons.CommandHandlers
{
	class CreateCouponTemplateCommandHandler : IRequestHandler<CreateCouponTemplateCommand>
	{
		private readonly ICouponTemplatesRepository repo;

		public CreateCouponTemplateCommandHandler(ICouponTemplatesRepository repo)
		{
			this.repo = repo;
		}

		public async Task<Unit> Handle(CreateCouponTemplateCommand cmd, CancellationToken cancellationToken)
		{
			repo.Save(new Entities.CouponTemplate
			{
				MealId = cmd.ProductId,
				PercentageDiscount = cmd.PercentageDiscount,
				ValidForDays = cmd.ValidForDays
			});

			return Unit.Value;
		}
	}
}
