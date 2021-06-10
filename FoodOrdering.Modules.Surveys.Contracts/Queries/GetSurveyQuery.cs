using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Surveys.Contracts.DTO;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Contracts.Queries
{
	public record GetSurveyQuery(Guid SurveyId) : IRequest<SurveyDTO>;
}
