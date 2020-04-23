using System.Collections.Generic;

namespace RestaurantOrder.Infrastructure.Repositories
{
	public interface IDishRepository
	{
		IEnumerable<DishRepository> GetAll();
		DishRepository GetById(int id);
	}
}
