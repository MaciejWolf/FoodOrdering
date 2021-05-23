using System;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Common.TestUtilities;
using FoodOrdering.Modules.OrderProcessing.Contracts.Events;
using FoodOrdering.Modules.Surveys.Contracts.Commands;
using FoodOrdering.Modules.Surveys.Contracts.Events;
using FoodOrdering.Modules.Surveys.Entities;
using FoodOrdering.Modules.Surveys.Handlers.Commands;
using FoodOrdering.Modules.Surveys.Handlers.Events;
using FoodOrdering.Modules.Surveys.Repositories;
using Xunit;

namespace FoodOrdering.Modules.Surveys.Tests
{
	public class CompleteSurveyCommandHandlerTests
	{
		private readonly ISurveyRepository surveyRepository = new InMemorySurveyRepository();
		private readonly PublisherMock publisherMock = new();

		private CompleteSurveyCommandHandler Handler
			=> new(surveyRepository, publisherMock.Object);

		private async Task Send(CompleteSurveyCommand cmd)
			=> await Handler.Handle(cmd, new System.Threading.CancellationToken());

		[Fact]
		public async Task WhenSurveyIsCompleted_EventIsPublished()
		{
			// Arrange
			var survey = await GenerateSurvey(Guid.NewGuid());

			var cmd = new CompleteSurveyCommand
			{
				SurveyId = survey.Id,
				Answers = survey.Questions.Select(q => new CompleteSurveyCommand.Answer
				{
					QuestionId = q.Id,
					Content = q.PossibleAnswers.First()
				}).ToArray()
			};

			// Act
			await Send(cmd);

			// Assert
			publisherMock.PublishedNotifications.OfType<SurveyCompletedEvent>().Single();
		}

		[Fact]
		public async Task SurveyCannotBeCompletedWithInvalidAnswers()
		{
			// Arrange
			var survey = await GenerateSurvey(Guid.NewGuid());

			var cmd = new CompleteSurveyCommand
			{
				SurveyId = survey.Id,
				Answers = survey.Questions.Select(q => new CompleteSurveyCommand.Answer
				{
					QuestionId = q.Id,
					Content = "xxx"
				}).ToArray()
			};

			// Act & Assert
			await Assert.ThrowsAsync<AppException>(() => Send(cmd));
		}

		private async Task<Survey> GenerateSurvey(Guid clientId)
		{
			var eventHandler = new OrderCompletedEventHandler(surveyRepository, publisherMock.Object);

			var e = new OrderCompletedEvent
			{
				ClientId = clientId,
				OrderId = Guid.NewGuid()
			};

			publisherMock.ClearPublishedNotifications();
			await eventHandler.Handle(e, new System.Threading.CancellationToken());
			var e2 = publisherMock.PublishedNotifications.OfType<SurveyGeneratedEvent>().Single();
			var id = e2.SurveyId;

			publisherMock.ClearPublishedNotifications();
			return surveyRepository.GetById(id);
		}
	}
}
