using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Flunt.Validations;

namespace RestaurantOrder.Domain.Commands
{
	public class CreateOrderCommand : Notifiable, ICommand
	{
		public string TimeOfDay { get; set; }
		public List<int> Dishes { get; set; }
		
		public void Validate()
		{
			// TODO: Passar uma lista de dishes null e verificar se vai executar o trecho que contém a query linq
			
			AddNotifications(new Contract()
				.Requires()
				.HasMinLen(TimeOfDay, 2, "Description.Name", "Name must contain at least 2 characters")
				.HasMaxLen(TimeOfDay, 30, "Description.Name", "Name must contain up to 30 characters")
			);

			AddNotifications(new Contract()
				.Requires()
				.IsNotNull(Dishes, "Dishes", "Order must contain at least one dish")
			);

			var quantityOfDishes = Dishes.Count;
			var amountOfInvalidDishes = Dishes
				.Where(p => p <= 0).ToList().Count;

			if (quantityOfDishes == amountOfInvalidDishes)
				AddNotification("Dishes", "Order must contain at least one dish");

			
			//if (Dishes == null)
			//{
			//	AddNotification("Dishes", "Order must contain at least one dish");
			//}
			//else
			//{
			//	var quantityOfDishes = Dishes.Count;
			//	var amountOfInvalidDishes = Dishes
			//		.Where(p => p <= 0).ToList().Count;

			//	if (quantityOfDishes == amountOfInvalidDishes)
			//		AddNotification("Dishes", "Order must contain at least one dish");
			//}
		}
	}
}
