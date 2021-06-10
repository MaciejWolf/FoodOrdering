using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Application.ViewModels;
using FoodOrdering.Modules.Basket.Contracts.Events;
using FoodOrdering.Modules.Basket.Domain.DomainServices;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.Models.Basket;
using FoodOrdering.Modules.Basket.Domain.Repositories;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Projections
{
	public class BasketProjection :
		INotificationHandler<BasketCreatedEvent>,
		INotificationHandler<ProductAddedEvent>,
		INotificationHandler<ProductRemovedFromBasketEvent>,
		INotificationHandler<ProductsQuantityChanged>,
		INotificationHandler<CouponAppliedEvent>,
		INotificationHandler<AppliedCouponRemovedEvent>,
		INotificationHandler<OrderPlacedEvent>
	{
		private readonly IProductsRepository productsRepository;
		private readonly ICouponsRepository couponsRepository;
		private readonly IViewModelsRepository repo;

		public BasketProjection(IProductsRepository productsRepository, ICouponsRepository couponsRepository, IViewModelsRepository repo)
		{
			this.productsRepository = productsRepository;
			this.couponsRepository = couponsRepository;
			this.repo = repo;
		}

		public async Task Handle(BasketCreatedEvent evnt, CancellationToken cancellationToken)
		{
			var basket = new BasketVm
			{
				Id = evnt.ClientId.ToGuid(),
				BasketItems = new(),
				Price = 0,
				AppliedCoupon = null
			};

			repo.Save(basket);
		}

		public async Task Handle(ProductAddedEvent evnt, CancellationToken cancellationToken)
		{
			repo.UpdateBasket(evnt.ClientId, basket =>
			{
				basket.BasketItems.Add(new BasketItem { ProductId = evnt.ProductId, Quantity = evnt.Quantity });
				UpdatePrice(basket);
			});
		}

		public async Task Handle(ProductRemovedFromBasketEvent evnt, CancellationToken cancellationToken)
		{
			repo.UpdateBasket(evnt.ClientId, basket =>
			{
				var item = basket.BasketItems.Single(bi => bi.ProductId == evnt.ProductId.ToGuid());
				basket.BasketItems.Remove(item);
				UpdatePrice(basket);
			});
		}

		public async Task Handle(ProductsQuantityChanged evnt, CancellationToken cancellationToken)
		{
			repo.UpdateBasket(evnt.ClientId, basket =>
			{
				var item = basket.BasketItems.Single(bi => bi.ProductId == evnt.ProductId.ToGuid());
				item.Quantity = evnt.Quantity;
				UpdatePrice(basket);
			});
		}

		public async Task Handle(CouponAppliedEvent evnt, CancellationToken cancellationToken)
		{
			repo.UpdateBasket(evnt.ClientId, basket =>
			{
				basket.AppliedCoupon = evnt.CouponId;
				UpdatePrice(basket);
			});
		}

		public async Task Handle(AppliedCouponRemovedEvent evnt, CancellationToken cancellationToken)
		{
			repo.UpdateBasket(evnt.ClientId, basket =>
			{
				basket.AppliedCoupon = null;
				UpdatePrice(basket);
			});
		}

		public async Task Handle(OrderPlacedEvent evnt, CancellationToken cancellationToken)
		{
			repo.UpdateBasket(evnt.UserId, basket =>
			{
				basket.AppliedCoupon = null;
				basket.BasketItems.Clear();
				basket.Price = 0;
			});
		}

		private void UpdatePrice(BasketVm basket)
		{
			var productQuantity = basket.BasketItems.Select(bi => (productsRepository.GetById(bi.ProductId), new Quantity(bi.Quantity)));

			Coupon coupon = null;

			if (basket.AppliedCoupon != null)
			{
				coupon = couponsRepository.GetById(basket.AppliedCoupon);
			}

			var price = PriceCalculator.Calculate(productQuantity, coupon);
			basket.Price = price.ToDecimal();
		}
	}
}
