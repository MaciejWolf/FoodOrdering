using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Basket.Contracts.Commands
{
	public record PlaceOrderCommand(Guid OrderId) : IRequest;
}
