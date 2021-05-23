using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Basket.Domain.Models.ValueObjects;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.Models.Order
{
	public class OrderAggregate : AggregateRoot<OrderId>
	{
		private readonly ClientId clientId;
		private readonly DateTime createdAt;
		private readonly DateTime validTo;

		private readonly List<OrderProduct> products;
		private readonly CouponId usedCoupon;

		public OrderAggregate(ClientId clientId, DateTime createdAt, List<OrderProduct> products, CouponId usedCoupon, Price totalPrice)
		{
			Id = Guid.NewGuid();
			this.clientId = clientId;
			this.createdAt = createdAt;
			validTo = createdAt.AddMinutes(5);
			this.products = products;
			this.usedCoupon = usedCoupon;
			TotalPrice = totalPrice;

		}

		public Price TotalPrice { get; }

		public DateTime ValidTo => validTo;

		public ClientId ClientId => clientId;

		public IEnumerable<OrderProduct> Products => products;

		public CouponId UsedCoupon => usedCoupon;

		public DateTime CreatedAt => createdAt;

		public bool IsPlaced { get; private set; }

		public void PlaceOrder(DateTime placingTime)
		{
			if (IsPlaced || placingTime > ValidTo)
			{
				throw new AppException("Order expired");
			}

			IsPlaced = true;
		}
	}

	public record OrderProduct(ProductId ProductId, Quantity Quantity);
}
