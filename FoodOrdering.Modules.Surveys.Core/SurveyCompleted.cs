using System;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Core
{
	public record SurveyCompleted(Guid Id, Guid UserId) : INotification;
}
