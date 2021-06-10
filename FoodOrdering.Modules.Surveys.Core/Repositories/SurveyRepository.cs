using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Surveys.Entities;

namespace FoodOrdering.Modules.Surveys.Repositories
{
	public class SurveyRepository : ISurveyRepository
	{
		private readonly SurveysDocumentStore documentStore;

		public SurveyRepository(SurveysDocumentStore documentStore)
		{
			this.documentStore = documentStore;
		}

		public Survey GetById(Guid surveyId)
		{
			using var session = documentStore.OpenSession();
			return session.Query<Survey>().Single(s => s.Id == surveyId);
		}

		public IEnumerable<Survey> GetForClient(Guid clientId)
		{
			using var session = documentStore.OpenSession();
			return session.Query<Survey>().Where(s => s.ClientId == clientId).ToArray();
		}

		public void Save(Survey survey)
		{
			using var session = documentStore.OpenSession();
			session.Store(survey);
			session.SaveChanges();
		}

		public void Update(Guid id, Action<Survey> updateOperation)
		{
			using var session = documentStore.OpenSession();
			var survey = session.Query<Survey>().Single(s => s.Id == id);
			updateOperation(survey);
			session.SaveChanges();
		}
	}
}
