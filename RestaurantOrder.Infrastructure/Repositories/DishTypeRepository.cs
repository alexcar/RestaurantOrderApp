using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.Infrastructure.Repositories
{
	public class DishTypeRepository : IDishTypeRepository
	{
		public int Id { get; set; }
		public string Description { get; set; }

		public IEnumerable<DishTypeRepository> GetAll()
		{
			return new List<DishTypeRepository>
			{
				new DishTypeRepository
				{
					Id = 1,
					Description = "Entrée"
				},
				new DishTypeRepository
				{
					Id = 2,
					Description = "Side"
				},
				new DishTypeRepository
				{
					Id = 3,
					Description = "Drink"
				},
				new DishTypeRepository
				{
					Id = 4,
					Description = "Dessert"
				}
			};
		}

		public DishTypeRepository GetById(int id)
		{
			var dishType = GetAll()
				.FirstOrDefault(p => p.Id == id);

			return dishType;
		}
	}
}
