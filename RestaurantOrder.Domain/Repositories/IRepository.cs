using System.Collections.Generic;
using RestaurantOrder.Domain.Entities;

namespace RestaurantOrder.Domain.Repositories
{
	public interface IRepository
	{
		IEnumerable<Dish> GetDishAll();
		IEnumerable<TimeOfDay> GetTimeOfDaysAll();
		IEnumerable<DishType> GetDishTypesAll();
		bool TimeOfDayExists(string description);
	}
}
