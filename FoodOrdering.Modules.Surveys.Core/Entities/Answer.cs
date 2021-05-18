using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Surveys.Entities
{
	class Answer
	{
		public Guid SurveyId { get; set; }
		public int QuestionId { get; set; }
		public string Content { get; set; }
	}
}
