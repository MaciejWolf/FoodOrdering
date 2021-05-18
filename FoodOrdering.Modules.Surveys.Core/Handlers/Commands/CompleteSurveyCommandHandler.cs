using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Surveys.Contracts.Commands;
using FoodOrdering.Modules.Surveys.Contracts.Events;
using FoodOrdering.Modules.Surveys.Entities;
using FoodOrdering.Modules.Surveys.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Handlers.Commands
{
	class CompleteSurveyCommandHandler : IRequestHandler<CompleteSurveyCommand>
	{
		private readonly ISurveyRepository surveyRepository;
		private readonly IPublisher publisher;

		public CompleteSurveyCommandHandler(ISurveyRepository surveyRepository, IPublisher publisher)
		{
			this.surveyRepository = surveyRepository;
			this.publisher = publisher;
		}

		public async Task<Unit> Handle(CompleteSurveyCommand request, CancellationToken cancellationToken)
		{
			var survey = surveyRepository.GetById(request.SurveyId);

			if (survey.Status != SurveyStatus.Open)
			{
				throw new Exception();
			}

			var answers = request.Answers.Select(a => new Answer
			{
				SurveyId = survey.Id,
				QuestionId = a.QuestionId,
				Content = a.Content
			});

			if (!survey.CanBeCompleted(answers))
			{
				throw new Exception();
			}

			survey.Status = SurveyStatus.Completed;

			await publisher.Publish(new SurveyCompletedEvent(survey.Id, survey.ClientId));

			return Unit.Value;
		}
	}
}
