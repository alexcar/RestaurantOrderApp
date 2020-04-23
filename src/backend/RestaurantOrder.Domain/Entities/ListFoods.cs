namespace RestaurantOrder.Domain.Entities
{
	public class ListFoods
	{
		public ListFoods(int? dishTypeId, string dishName)
		{
			DishTypeId = dishTypeId;
			DishName = dishName;
		}
		
		public int? DishTypeId { get; private set; }
		public string DishName { get; private set; }
		
	}
}
