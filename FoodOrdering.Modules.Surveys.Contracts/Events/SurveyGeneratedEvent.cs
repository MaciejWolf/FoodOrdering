using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Contracts.Events
{
	public record SurveyGeneratedEvent(Guid SurveyId) : INotification;
}
