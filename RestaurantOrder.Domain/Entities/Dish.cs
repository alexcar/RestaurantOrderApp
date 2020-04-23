using RestaurantOrder.Domain.ValueObjects;

namespace RestaurantOrder.Domain.Entities
{
	public class Dish
	{
		public Dish(
			int id, 
			Description description, 
			bool isMultiple, 
			int dishTypeId,
			DishType dishType,
			int timeOfDayId,
			TimeOfDay timeOfDay)
		{
			Id = id;
			Description = description;
			IsMultiple = isMultiple;
			DishTypeId = dishTypeId;
			DishType = dishType;
			TimeOfDayId = timeOfDayId;
			TimeOfDay = timeOfDay;
		}

		public int Id { get; private set; }
		public Description Description { get; private set; }
		public bool IsMultiple { get; private set; }
		public int DishTypeId { get; private set; }
		public DishType DishType { get; private set; }
		public int TimeOfDayId { get; private set; }
		public TimeOfDay TimeOfDay { get; private set; }
	}
}
