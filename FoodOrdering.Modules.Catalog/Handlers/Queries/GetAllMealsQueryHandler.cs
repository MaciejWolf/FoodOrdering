using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodOrdering.Modules.Catalog.Contracts.DTO;
using FoodOrdering.Modules.Catalog.Contracts.Queries;
using FoodOrdering.Modules.Catalog.Repositories;
using MediatR;

namespace FoodOrdering.Modules.Catalog.Handlers.Queries
{
	public class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQuery, IReadOnlyCollection<MealDTO>>
	{
		private readonly IMealsRepository mealsRepository;

		public GetAllMealsQueryHandler(IMealsRepository mealsRepository)
		{
			this.mealsRepository = mealsRepository;
		}

		public async Task<IReadOnlyCollection<MealDTO>> Handle(GetAllMealsQuery request, CancellationToken cancellationToken)
		{
			var meals = await mealsRepository.GetAll();
			var dtos = meals.Select(m => m.ToDTO()).ToArray();
			return new ReadOnlyCollection<MealDTO>(dtos);
		}
	}
}
