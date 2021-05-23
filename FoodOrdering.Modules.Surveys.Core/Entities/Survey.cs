using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrdering.Modules.Surveys.Entities
{
	public class Survey
	{
		public Guid Id { get; set; }
		public Guid ClientId { get; set; }
		public SurveyStatus Status { get; set; }
		public IEnumerable<Question> Questions { get; set; }

		public bool CanBeCompleted(IEnumerable<Answer> answers)
		{
			//if (!answers.All(a => a.SurveyId == Id))
			//	return false;

			if (!AreQuestionIdsValid(answers.Select(a => a.QuestionId)))
				return false;

			foreach (var answer in answers)
			{
				if (!Questions.Single(q => q.Id == answer.QuestionId).PossibleAnswers.Contains(answer.Content))
					return false;
			}

			return true;
		}

		private bool AreQuestionIdsValid(IEnumerable<int> ids) => Questions.Select(q => q.Id).SequenceEqual(ids);
	}

	public enum SurveyStatus
	{
		Open,
		Completed
	}
}
