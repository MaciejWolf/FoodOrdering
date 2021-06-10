using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Basket.Domain.Models;
using FoodOrdering.Modules.Basket.Domain.ValueObjects;

namespace FoodOrdering.Modules.Basket.Domain.DomainServices
{
	public class PriceCalculator
	{
		public static Price Calculate(IEnumerable<(Product, Quantity)> productQuantityPairs, Coupon coupon)
		{
			var total = Price.Zero;

			foreach (var pq in productQuantityPairs)
			{
				for (var i = 0; i < pq.Item2.ToInt(); i++)
				{
					total += pq.Item1.Price;
				}
			}

			if (coupon is not null)
				total -= coupon.Value;

			return total;
		}

		public static Price Calculate(IEnumerable<Product> products, Coupon coupon)
		{
			var total = Price.Zero;

			foreach (var price in products.Select(p => p.Price))
			{
				total += price;
			}

			if (coupon is not null)
				total -= coupon.Value;

			return total;
		}
	}
}
