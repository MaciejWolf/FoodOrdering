using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Auth.Contracts.Events
{
	public record UserRegisteredEvent(Guid UserId) : INotification;
}
