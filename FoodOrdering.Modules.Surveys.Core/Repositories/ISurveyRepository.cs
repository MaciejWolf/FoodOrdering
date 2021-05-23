using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Surveys.Entities;

namespace FoodOrdering.Modules.Surveys.Repositories
{
	public interface ISurveyRepository
	{
		public void Save(Survey survey);
		Survey GetById(Guid surveyId);
		IEnumerable<Survey> GetForClient(Guid clientId);
	}
}
