using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Contracts.DTO;
using MediatR;

namespace FoodOrdering.Modules.OrderProcessing.Contracts.Queries
{
	public record GetActiveOrdersQuery : IRequest<IEnumerable<OrderDTO>>;
}
