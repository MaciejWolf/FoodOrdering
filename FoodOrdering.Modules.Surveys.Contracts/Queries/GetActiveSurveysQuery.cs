using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodOrdering.Modules.Surveys.Contracts.Queries
{
	public record GetActiveSurveysQuery(Guid ClientId) : IRequest<IEnumerable<Guid>>;
}
