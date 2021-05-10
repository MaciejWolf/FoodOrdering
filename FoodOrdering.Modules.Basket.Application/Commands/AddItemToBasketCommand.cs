using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Core.Entities;
using FoodOrdering.Modules.Basket.Core.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Baskets.Commands
{
	public class AddItemToBasketCommand : IRequest
	{
		public Guid UserId { get; set; }
		public Guid ItemId { get; set; }
		public int Quantity { get; set; }

		public class Handler : IRequestHandler<AddItemToBasketCommand>
		{
			private readonly IMealsRepository mealsRepository;
			private readonly IUsersRepository usersRepository;

			public async Task<Unit> Handle(AddItemToBasketCommand cmd, CancellationToken cancellationToken)
			{
				if (!mealsRepository.Exists(cmd.ItemId))
				{
					throw new Exception();
				}

				var user = usersRepository.Get(cmd.UserId);
				if (user.Basket.BasketItems.Any(i => i.ItemId == cmd.ItemId))
				{
					var basketItem = user.Basket.BasketItems.Single(i => i.ItemId == cmd.ItemId);

					basketItem.Quantity += cmd.Quantity;
					usersRepository.Update(user);
				}
				else
				{
					user.Basket.BasketItems.Add(new BasketItem
					{
						ItemId = cmd.ItemId,
						Quantity = cmd.Quantity
					});
				}

				usersRepository.Update(user);

				return Unit.Value;
			}
		}
	}
}
