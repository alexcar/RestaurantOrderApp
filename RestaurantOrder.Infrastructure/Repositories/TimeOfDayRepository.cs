using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.Infrastructure.Repositories
{
	public class TimeOfDayRepository : ITimeOfDayRepository
	{
		public int Id { get; set; }
		public string Description { get; set; }

		public IEnumerable<TimeOfDayRepository> GetAll()
		{
			return new List<TimeOfDayRepository>
			{
				new TimeOfDayRepository
				{
					Id = 1,
					Description = "Morning"
				},
				new TimeOfDayRepository
				{
					Id = 2,
					Description = "Night"
				}
			};
		}

		public TimeOfDayRepository GetById(int id)
		{
			var timeOfDay = GetAll()
				.FirstOrDefault(p => p.Id == id);

			return timeOfDay;
		}

		public TimeOfDayRepository GetByDescription(string description)
		{
			var timeOfDay = GetAll()
				.FirstOrDefault(
				p => p.Description.ToLower().Contains(description.ToLower()));

			return timeOfDay;
		}
	}
}
