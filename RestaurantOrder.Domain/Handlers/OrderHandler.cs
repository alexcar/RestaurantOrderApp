using System.Collections.Generic;
using System.Data;
using System.Linq;
using Flunt.Notifications;
using RestaurantOrder.Domain.Commands;
using RestaurantOrder.Domain.Entities;
using RestaurantOrder.Domain.Repositories;

namespace RestaurantOrder.Domain.Handlers
{
	public class OrderHandler :Notifiable, IHandler<CreateOrderCommand>
	{
		private readonly IRepository _repository;

		public OrderHandler(IRepository repository)
		{
			_repository = repository;
		}
		
		public ICommandResult Handle(CreateOrderCommand command)
		{
			var message = string.Empty;
			const string ERROR = "error";
			
			// Checks whether the requisition is valid
			command.Validate();

			if (command.Invalid)
			{
				AddNotifications(command);
				return new CommandResult(false, "Your order could not be placed");
			}

			// Checks in the repository if the time of day exists
			if (!_repository.TimeOfDayExists(command.TimeOfDay))
			{
				AddNotifications(command);
				return new CommandResult(false, "The time of day entered is invalid");
			}

			var duplicateDishes = new List<string>();
			var listFoods = new List<ListFoods>();
			
			foreach (var dishId in command.Dishes)
			{
				// search the repository for dishes
				var dish = _repository
					.GetDishAll()
					.Where(p => p.Id == dishId)
					.FirstOrDefault(p => p.TimeOfDay.Description.Name.ToLower()
						.Equals(command.TimeOfDay.ToLower()));

				if (dish == null)
				{
					// Retrieves the last index
					var lastIndex = listFoods.Max(p => p.DishTypeId);
					
					lastIndex = lastIndex == null ? 0 : ++lastIndex;
					listFoods.Add(new ListFoods(lastIndex, ERROR));
					break;
				}
				else
				{
					// Controls duplicate dishes
					var multipleFood = duplicateDishes
						.Where(p => p.ToLower().Contains(dish.Description.Name.ToLower()))
						.ToList()
						.Count;

					if (multipleFood > 0)
					{
						if (dish.IsMultiple)
						{
							listFoods.Remove(
								listFoods
									.FirstOrDefault(
										p => p.DishName.ToLower().Contains(dish.Description.Name.ToLower())));
							listFoods.Add(
								new ListFoods(
									dish.DishType.Id, $"{dish.Description.Name.ToLower()}(x{++multipleFood})"));

							duplicateDishes.Add(dish.Description.Name.ToLower());
						}
						else
						{
							// Retrieves the last index
							var lastIndex = listFoods.Max(p => p.DishTypeId);
							
							lastIndex = lastIndex == null ? 0 : ++lastIndex;
							listFoods.Add(new ListFoods(lastIndex, ERROR));
							break;
						}
					}
					else
					{
						listFoods.Add(new ListFoods(dish.DishType.Id, dish.Description.Name.ToLower()));
						duplicateDishes.Add(dish.Description.Name.ToLower());
					}
				}
			}

			var foods = string.Join(", ", listFoods
				.OrderBy(p => p.DishTypeId).Select(p => p.DishName));

			var quantityOfDishes = listFoods.Count;
			var errorOccurred = listFoods.Where(p => p.DishName.Contains(ERROR)).ToList().Count;

			if (errorOccurred == 0)
				message = "Your order has been successfully placed";
			else
			{
				message = quantityOfDishes > 1 ?
					"Your order was partially placed" :
					"Your order could not be placed";
			}

			return new CommandResult(true, message, foods);
		}
	}
}
