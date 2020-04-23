using System.Collections.Generic;
using RestaurantOrder.Domain.Entities;
using RestaurantOrder.Domain.Repositories;
using RestaurantOrder.Domain.ValueObjects;

namespace RestaurantOrder.Infrastructure.Repositories
{
	public class Repository : IRepository
	{
		private readonly IDishRepository _dishRepository;
		private readonly ITimeOfDayRepository _timeOfDayRepository;
		private readonly IDishTypeRepository _dishTypeRepository;


		public Repository(
			IDishRepository dishRepository,
			ITimeOfDayRepository timeOfDayRepository,
			IDishTypeRepository dishTypeRepository)
		{
			_dishRepository = dishRepository;

			_timeOfDayRepository = timeOfDayRepository;
			_dishTypeRepository = dishTypeRepository;
		}

		public IEnumerable<Dish> GetDishAll()
		{
			var dishes = _dishRepository.GetAll();

			return Map(dishes);
		}

		public IEnumerable<TimeOfDay> GetTimeOfDaysAll()
		{
			var timeOfDay = _timeOfDayRepository.GetAll();
			return Map(timeOfDay);
		}

		public IEnumerable<DishType> GetDishTypesAll()
		{
			var dishTypes = _dishTypeRepository.GetAll();

			return Map(dishTypes);
		}

		public bool TimeOfDayExists(string description)
		{
			var timeOfDay = _timeOfDayRepository.GetByDescription(description);
			return timeOfDay != null;
		}

		private IEnumerable<Dish> Map(IEnumerable<DishRepository> dishesRepository)
		{
			if (dishesRepository == null)
				return null;
			
			var dishes = new List<Dish>();

			foreach (var dishRepository in dishesRepository)
			{
				var dish = new Dish(
					dishRepository.Id,
					new Description(dishRepository.Description),
					dishRepository.IsMultiple,
					dishRepository.DishTypeId,
					new DishType(
						dishRepository.DishType.Id, 
						new Description(dishRepository.DishType.Description)),
					dishRepository.TimeOfDayId,
					new TimeOfDay(
						dishRepository.TimeOfDay.Id, 
						new Description(dishRepository.TimeOfDay.Description))
				);

				dishes.Add(dish);
			}

			return dishes;
		}

		private List<TimeOfDay> Map(IEnumerable<TimeOfDayRepository> timesOfDayRepository)
		{
			if (timesOfDayRepository == null)
				return null;

			var timesOfDay = new List<TimeOfDay>();
			
			foreach (var timeOfDayRepository in timesOfDayRepository)
			{
				var timeOfDay = new TimeOfDay(
					timeOfDayRepository.Id,
					new Description(timeOfDayRepository.Description)
					);

				timesOfDay.Add(timeOfDay);
			}

			return timesOfDay;
		}

		private List<DishType> Map(IEnumerable<DishTypeRepository> dishTypesRepository)
		{
			if (dishTypesRepository == null)
				return null;

			var dishTypes = new List<DishType>();

			foreach (var dishTypeRepository in dishTypesRepository)
			{
				var dishType = new DishType(
					dishTypeRepository.Id,
					new Description(dishTypeRepository.Description)
					);

				dishTypes.Add(dishType);
			}

			return dishTypes;
		}
	}
}
