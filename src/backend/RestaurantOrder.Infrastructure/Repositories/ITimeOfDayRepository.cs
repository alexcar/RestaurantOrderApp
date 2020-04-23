using System.Collections.Generic;

namespace RestaurantOrder.Infrastructure.Repositories
{
	public interface ITimeOfDayRepository
	{
		IEnumerable<TimeOfDayRepository> GetAll();
		TimeOfDayRepository GetById(int id);
		TimeOfDayRepository GetByDescription(string description);
	}
}
