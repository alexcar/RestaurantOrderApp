using System.Collections.Generic;
using RestaurantOrder.Domain.ValueObjects;

namespace RestaurantOrder.Domain.Entities
{
	public class TimeOfDay
	{
		public TimeOfDay(int id, Description description)
		{
			Id = id;
			Description = description;
		}
		
		public int Id { get; private set; }
		public Description Description { get; private set; }
	}
}
