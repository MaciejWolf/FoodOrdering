﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.OrderProcessing.Contracts.Commands
{
	public record RejectOrderCommand(Guid OrderId) : IRequest;
}
