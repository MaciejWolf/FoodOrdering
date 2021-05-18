using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Modules.Surveys.Contracts.DTO
{
	public class SurveyDTO
	{
		public Guid Id { get; set; }
		public IEnumerable<QuestionDTO> Questions { get; set; }
	}

	public class QuestionDTO
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public IEnumerable<string> PossibleAnswers { get; set; }
	}
}
