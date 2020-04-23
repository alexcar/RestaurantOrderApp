using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.Infrastructure.Repositories
{
	public class DishRepository : IDishRepository
	{
		private readonly IDishTypeRepository _dishTypeRepository;
		private readonly ITimeOfDayRepository _timeOfDayRepository;

		public DishRepository(IDishTypeRepository dishTypeRepository, ITimeOfDayRepository timeOfDayRepository)
		{
			TimeOfDay = new TimeOfDayRepository();
			DishType = new DishTypeRepository();
			_dishTypeRepository = dishTypeRepository;
			_timeOfDayRepository = timeOfDayRepository;
		}
		
		public int Id { get; set; }
		public string Description { get; set; }
		public bool IsMultiple { get; set; }
		public int DishTypeId { get; set; }
		public DishTypeRepository DishType { get; set; }
		public int TimeOfDayId { get; set; }
		public TimeOfDayRepository TimeOfDay { get; set; }

		public IEnumerable<DishRepository> GetAll()
		{
			return new List<DishRepository>
			{
				new DishRepository(new DishTypeRepository(), new TimeOfDayRepository())
				{
					Id = 1,
					Description = "Eggs",
					IsMultiple = false,
					DishTypeId = 1,
					DishType = _dishTypeRepository.GetById(1),
					TimeOfDayId = 1,
					TimeOfDay = _timeOfDayRepository.GetById(1)
				},
				new DishRepository(new DishTypeRepository(), new TimeOfDayRepository())
				{
					Id = 2,
					Description = "Toast",
					IsMultiple = false,
					DishTypeId = 2,
					DishType = _dishTypeRepository.GetById(2),
					TimeOfDayId = 1,
					TimeOfDay = _timeOfDayRepository.GetById(1)
				},
				new DishRepository(new DishTypeRepository(), new TimeOfDayRepository())
				{
					Id = 3,
					Description = "Coffee",
					IsMultiple = true,
					DishTypeId = 3,
					DishType = _dishTypeRepository.GetById(3),
					TimeOfDayId = 1,
					TimeOfDay = _timeOfDayRepository.GetById(1)
				},
				new DishRepository(new DishTypeRepository(), new TimeOfDayRepository())
				{
					Id = 1,
					Description = "Steak",
					IsMultiple = false,
					DishTypeId = 1,
					DishType = _dishTypeRepository.GetById(1),
					TimeOfDayId = 2,
					TimeOfDay = _timeOfDayRepository.GetById(2)
				},
				new DishRepository(new DishTypeRepository(), new TimeOfDayRepository())
				{
					Id = 2,
					Description = "Potato",
					IsMultiple = true,
					DishTypeId = 2,
					DishType = _dishTypeRepository.GetById(2),
					TimeOfDayId = 2,
					TimeOfDay = _timeOfDayRepository.GetById(2)
				},
				new DishRepository(new DishTypeRepository(), new TimeOfDayRepository())
				{
					Id = 3,
					Description = "Wine",
					IsMultiple = false,
					DishTypeId = 3,
					DishType = _dishTypeRepository.GetById(3),
					TimeOfDayId = 2,
					TimeOfDay = _timeOfDayRepository.GetById(2)
				},
				new DishRepository(new DishTypeRepository(), new TimeOfDayRepository())
				{
					Id = 4,
					Description = "Cake",
					IsMultiple = false,
					DishTypeId = 4,
					DishType = _dishTypeRepository.GetById(4),
					TimeOfDayId = 2,
					TimeOfDay = _timeOfDayRepository.GetById(2)
				}
			};
		}

		public DishRepository GetById(int id)
		{
			var dishRepository = GetAll()
				.FirstOrDefault(p => p.Id == id);

			return dishRepository;
		}
	}
}
