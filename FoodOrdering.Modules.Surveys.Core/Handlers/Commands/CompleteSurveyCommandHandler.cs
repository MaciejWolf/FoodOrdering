using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Surveys.Contracts.Commands;
using FoodOrdering.Modules.Surveys.Contracts.Events;
using FoodOrdering.Modules.Surveys.Entities;
using FoodOrdering.Modules.Surveys.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Handlers.Commands
{
	public class CompleteSurveyCommandHandler : IRequestHandler<CompleteSurveyCommand>
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
			SurveyCompletedEvent evnt = null;

			surveyRepository.Update(request.SurveyId, survey =>
			{
				if (survey.Status != SurveyStatus.Open)
				{
					throw new AppException();
				}

				var answers = request.Answers.Select(a => new Answer
				{
					QuestionId = a.QuestionId,
					Content = a.Content
				});

				if (!survey.CanBeCompleted(answers))
				{
					throw new AppException();
				}

				survey.Status = SurveyStatus.Completed;

				evnt = new SurveyCompletedEvent(survey.Id, survey.ClientId);
			});

			await publisher.Publish(evnt);

			return Unit.Value;
		}
	}
}
