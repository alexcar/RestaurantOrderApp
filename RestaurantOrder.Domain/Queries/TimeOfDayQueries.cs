using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RestaurantOrder.Domain.Entities;
using RestaurantOrder.Domain.Repositories;

namespace RestaurantOrder.Domain.Queries
{
	public class TimeOfDayQueries : ITimeOfDayQueries
	{
		private readonly IRepository _repository;

		public TimeOfDayQueries(IRepository repository)
		{
			_repository = repository;
		}
		
		public static Expression<Func<TimeOfDay, bool>> GetTimeOfDayByName(string name)
		{
			return p => p.Description.Name == name;
		}

		public IEnumerable<TimeOfDayResult> GetTimeOfDayAll()
		{
			return Map(_repository.GetTimeOfDaysAll());
		}

		private List<TimeOfDayResult> Map(IEnumerable<TimeOfDay> timesOfDay)
		{
			if (timesOfDay == null)
				return null;

			var timesOfDayResponse = new List<TimeOfDayResult>();

			foreach (var timeOfDay in timesOfDay)
			{
				var timeOfDayResponse = new TimeOfDayResult()
				{
					Id = timeOfDay.Id,
					Name = timeOfDay.Description.Name
				};

				timesOfDayResponse.Add(timeOfDayResponse);
			}

			return timesOfDayResponse;
		}
	}
}
