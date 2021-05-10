using System;
using MediatR;

namespace FoodOrdering.Modules.Users.Contracts.Events
{
	public record UserSignedUpEvent(Guid UserId, string Email) : INotification;
}
