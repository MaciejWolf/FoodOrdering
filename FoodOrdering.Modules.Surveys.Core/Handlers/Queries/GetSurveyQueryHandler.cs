using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Surveys.Contracts.DTO;
using FoodOrdering.Modules.Surveys.Contracts.Queries;
using FoodOrdering.Modules.Surveys.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Handlers.Queries
{
	public class GetSurveyQueryHandler : IRequestHandler<GetSurveyQuery, SurveyDTO>
	{
		private readonly ISurveyRepository surveyRepository;

		public GetSurveyQueryHandler(ISurveyRepository surveyRepository)
		{
			this.surveyRepository = surveyRepository;
		}

		public async Task<SurveyDTO> Handle(GetSurveyQuery request, CancellationToken cancellationToken)
		{
			var survey = surveyRepository.GetById(request.SurveyId);
			var dto = new SurveyDTO
			{
				Id = survey.Id,
				Questions = survey.Questions.Select(q => new QuestionDTO
				{
					Id = q.Id,
					Content = q.Content,
					PossibleAnswers = q.PossibleAnswers
				})
			};

			return dto;
		}
	}
}
