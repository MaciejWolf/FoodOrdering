using System;
using System.Collections.Generic;

namespace FoodOrdering.Modules.Basket.Core.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public List<Discount> GrantedDiscounts { get; set; }
		public List<Coupon> GrantedCoupons { get; set; }
		public List<BasketItem> BasketItems { get; set; }
		public Discount AppliedDiscount { get; set; }
		public List<Coupon> AppliedCoupons { get; set; }

		public void CreateOrder()
		{

		}
	}

	public class Discount
	{
		public Guid Id { get; set; }
		public int Percentage { get; set; }
	}

	public class Coupon 
	{
		public Guid Id { get; set; }
		public int Percentage { get; set; }
		public DateTime ValidTo { get; set; }
	}

	public class Basket
	{
		public List<BasketItem> BasketItems { get; set; }
		public Discount Discout { get; set; }
		public Coupon Coupon { get; set; }
	}

	public class BasketItem // ValueObject
	{
		public Guid ItemId { get; set; }
		public int Quantity { get; set; }
	}

	public class Order
	{
		public List<BasketItem> BasketItems { get; set; }
		public Discount Discout { get; set; }
		public Coupon Coupon { get; set; }
	}
}
