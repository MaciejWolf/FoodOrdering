using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common;
using FoodOrdering.Modules.Surveys.Entities;

namespace FoodOrdering.Modules.Surveys.Repositories
{
	public class InMemorySurveyRepository : ISurveyRepository
	{
		private readonly List<Survey> surveys = new();

		public Survey GetById(Guid surveyId)
		{
			return surveys.SingleOrDefault(s => s.Id == surveyId);
		}

		public IEnumerable<Survey> GetForClient(Guid clientId)
		{
			return surveys.Where(s => s.ClientId == clientId);
		}

		public void Save(Survey survey)
		{
			if (surveys.Select(s => s.Id).Contains(survey.Id))
				throw new AppException("Srvey already exists");

			surveys.Add(survey);
		}
	}
}
