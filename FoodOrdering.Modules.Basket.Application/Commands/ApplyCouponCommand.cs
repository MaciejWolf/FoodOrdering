using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Basket.Application.Commands
{
	public class ApplyCouponCommand : IRequest
	{
		public class Handler : IRequestHandler<ApplyCouponCommand>
		{
			public Task<Unit> Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
			{
				throw new NotImplementedException();
			}
		}
	}
}
