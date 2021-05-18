using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Contracts.Commands
{
	public class CompleteSurveyCommand : IRequest
	{
		public Guid SurveyId { get; set; }
		public Answer[] Answers { get; set; }

		public class Answer
		{
			public int QuestionId { get; set; }
			public string Content { get; set; }
		}
	}
}
