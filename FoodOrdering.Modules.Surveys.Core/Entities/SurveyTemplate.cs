using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Surveys.Entities
{
	class SurveyTemplate
	{
		public IEnumerable<Question> Questions { get; set; }

		public class Question
		{
			public int Id { get; set; }
			public string Content { get; set; }
			public IEnumerable<string> PossibleAnswers { get; set; }
		}

		public Survey OpenSurvey(Guid id, Guid clientId)
		{
			return new Survey
			{
				Id = id,
				ClientId = clientId,
				Questions = Questions.Select(q => new Entities.Question
				{
					Id = q.Id,
					Content = q.Content,
					PossibleAnswers = q.PossibleAnswers
				})
			};
		}
	}
}
