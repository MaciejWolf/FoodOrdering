using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Users.Contracts.Events
{
	public record UserSignedInEvent(Guid UserId) : INotification;
}
