using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrder.Infrastructure.Repositories
{
	public interface IDishTypeRepository
	{
		IEnumerable<DishTypeRepository> GetAll();
		DishTypeRepository GetById(int id);
	}
}
