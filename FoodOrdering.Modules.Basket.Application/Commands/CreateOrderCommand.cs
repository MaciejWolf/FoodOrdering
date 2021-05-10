using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Commands
{
	public class CreateOrderCommand : IRequest
	{
		public class Handler : IRequestHandler<CreateOrderCommand>
		{
			public Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
			{
				
			}
		}
	}
}
