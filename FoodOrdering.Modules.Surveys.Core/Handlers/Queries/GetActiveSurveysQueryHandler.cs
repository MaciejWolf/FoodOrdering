using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Surveys.Contracts.Queries;
using FoodOrdering.Modules.Surveys.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Handlers.Queries
{
	public class GetActiveSurveysQueryHandler : IRequestHandler<GetActiveSurveysQuery, IEnumerable<Guid>>
	{
		private readonly ISurveyRepository surveyRepository;

		public GetActiveSurveysQueryHandler(ISurveyRepository surveyRepository)
		{
			this.surveyRepository = surveyRepository;
		}

		public async Task<IEnumerable<Guid>> Handle(GetActiveSurveysQuery request, CancellationToken cancellationToken)
		{
			return surveyRepository
				.GetForClient(request.ClientId)
				.Where(s => s.Status == Entities.SurveyStatus.Open)
				.Select(s => s.Id);
		}
	}
}
