using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantOrder.Domain.Commands;
using RestaurantOrder.Domain.Handlers;
using RestaurantOrder.Infrastructure.Repositories;

namespace Restaurant.Order.Tests
{
	[TestClass]
	public class CreateOrderTests
	{
		[TestMethod]
		public void CreateOrder_Cannot_Be_Null()
		{
			var command = new CreateOrderCommand();
			var dishes = new List<int>();

			command.Dishes = dishes;

			var handler = new OrderHandler(
				new Repository(
					new DishRepository(
						new DishTypeRepository(),
						new TimeOfDayRepository()),
					new TimeOfDayRepository(),
					new DishTypeRepository()));

			var result = (CommandResult)handler.Handle(command);

			Assert.AreEqual(false, result.Success);
		}

		[TestMethod]
		public void Order_Must_Have_Time_Of_Day()
		{
			var command = new CreateOrderCommand();

			var dishes = new List<int>()
			{
				1, 2, 3
			};

			command.Dishes = dishes;

			var handler = new OrderHandler(
				new Repository(
					new DishRepository(new DishTypeRepository(), new TimeOfDayRepository()),
					new TimeOfDayRepository(),
					new DishTypeRepository()));

			var result = (CommandResult)handler.Handle(command);

			Assert.AreEqual(false, result.Success);
		}

		[TestMethod]
		public void Order_Must_Have_An_Existing_Day_Time()
		{
			var command = new CreateOrderCommand
			{
				TimeOfDay = "dawn"
			};

			var dishes = new List<int>()
			{
				1, 2, 3
			};

			command.Dishes = dishes;

			var handler = new OrderHandler(
				new Repository(
					new DishRepository(new DishTypeRepository(), new TimeOfDayRepository()),
					new TimeOfDayRepository(),
					new DishTypeRepository()));

			var result = (CommandResult)handler.Handle(command);

			Assert.AreEqual(false, result.Success);
		}

		[TestMethod]
		public void Order_Must_Have_At_Least_One_Dish()
		{
			var command = new CreateOrderCommand
			{
				TimeOfDay = "morning"
			};

			var dishes = new List<int>();

			command.Dishes = dishes;

			var handler = new OrderHandler(
				new Repository(
					new DishRepository(new DishTypeRepository(), new TimeOfDayRepository()),
					new TimeOfDayRepository(),
					new DishTypeRepository()));

			var result = (CommandResult)handler.Handle(command);

			Assert.AreEqual(false, result.Success);
		}

		[TestMethod]
		[DataRow("morning", new int[] {1,2,3}, "eggs, toast, coffee")]
		[DataRow("morning", new int[] {2,1,3}, "eggs, toast, coffee")]
		[DataRow("morning", new int[] {1,2,3,4}, "eggs, toast, coffee, error")]
		[DataRow("morning", new int[] {1,2,3,3,3}, "eggs, toast, coffee(x3)")]
		[DataRow("night", new int[] { 1, 2, 3, 4 }, "steak, potato, wine, cake")]
		[DataRow("night", new int[] { 1, 2, 2, 4 }, "steak, potato(x2), cake")]
		[DataRow("night", new int[] { 1, 2, 3, 5 }, "steak, potato, wine, error")]
		[DataRow("night", new int[] { 1, 1, 2, 3, 5 }, "steak, error")]
		public void Order_Must_Be_Sent_Successfully(
			string timeOfDay,
			int[] inputDishes,
			string outputFoods)
		{
			var command = new CreateOrderCommand
			{
				TimeOfDay = timeOfDay
			};

			var dishes = new List<int>();
			foreach (var dish in inputDishes)
			{
				dishes.Add(dish);
			}

			command.Dishes = dishes;

			var handler = new OrderHandler(
				new Repository(
					new DishRepository(new DishTypeRepository(), new TimeOfDayRepository()),
					new TimeOfDayRepository(),
					new DishTypeRepository()));

			var result = (CommandResult)handler.Handle(command);

			Assert.AreEqual(outputFoods, result.Foods);
		}
	}
}
