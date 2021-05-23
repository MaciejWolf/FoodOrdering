using System.Collections.Generic;

namespace FoodOrdering.Modules.Surveys.Entities
{
	public class Question
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public IEnumerable<string> PossibleAnswers { get; set; }
	}
}
