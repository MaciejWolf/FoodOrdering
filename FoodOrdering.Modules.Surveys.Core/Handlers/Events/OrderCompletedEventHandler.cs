using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.OrderProcessing.Contracts.Events;
using FoodOrdering.Modules.Surveys.Entities;
using FoodOrdering.Modules.Surveys.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Handlers.Events
{
	class OrderCompletedEventHandler : INotificationHandler<OrderCompletedEvent>
	{
		private readonly ISurveyRepository surveyRepository;

		public OrderCompletedEventHandler(ISurveyRepository surveyRepository)
		{
			this.surveyRepository = surveyRepository;
		}

		public async Task Handle(OrderCompletedEvent notification, CancellationToken cancellationToken)
		{
			var survey = SurveyTemplate.OpenSurvey(Guid.NewGuid(), notification.ClientId);

			surveyRepository.Save(survey);
		}

		private static SurveyTemplate SurveyTemplate => new SurveyTemplate
		{
			Questions = new[]
				{
					new SurveyTemplate.Question
					{
						Id = 0,
						Content = "Are you satisfied with the meal?",
						PossibleAnswers = new[]
						{
							"yes",
							"no"
						}
					},
					new SurveyTemplate.Question
					{
						Id = 1,
						Content = "Are you satisfied with the service?",
						PossibleAnswers = new[]
						{
							"yes",
							"no"
						}
					},
					new SurveyTemplate.Question
					{
						Id = 2,
						Content = "Would you recommend us to your friend?",
						PossibleAnswers = new[]
						{
							"yes",
							"no"
						}
					},
				}
		};
	}
}
