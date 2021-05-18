using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;
using FoodOrdering.Modules.Basket.Repositories;
using FoodOrdering.Modules.Coupons.Contracts.Events;
using MediatR;

namespace FoodOrdering.Modules.Basket.Handlers.Events
{
	class CouponGrantedEventHandler : INotificationHandler<CouponGrantedEvent>
	{
		private readonly IBasketsRepository basketsRepository;
		private readonly IProductsRepository productsRepository;

		public CouponGrantedEventHandler(IBasketsRepository basketsRepository, IProductsRepository productsRepository)
		{
			this.basketsRepository = basketsRepository;
			this.productsRepository = productsRepository;
		}

		public async Task Handle(CouponGrantedEvent evnt, CancellationToken cancellationToken)
		{
			var basket = basketsRepository.GetById(evnt.UserId);

			var product = await productsRepository.GetById(evnt.ProductId);

			basket.AddCoupon(evnt.CouponId, evnt.ProductId, product.BasePrice, Discount.Percent(evnt.PercentageDiscount), evnt.ValidTo);
		}
	}
}
