using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Contracts.DTO;
using MediatR;

namespace FoodOrdering.Modules.Catalog.Contracts.Queries
{
	public record GetAllMealsQuery : IRequest<IReadOnlyCollection<MealDTO>>;
}
