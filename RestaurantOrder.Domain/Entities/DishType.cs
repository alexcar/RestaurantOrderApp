using RestaurantOrder.Domain.ValueObjects;

namespace RestaurantOrder.Domain.Entities
{
	public class DishType
	{
		public DishType(int id, Description description)
		{
			Id = id;
			Description = description;
		}
		
		public int Id { get; private set; }
		public Description Description { get; private set; }
	}
}
