using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrder.Domain.Queries
{
	public interface ITimeOfDayQueries
	{
		IEnumerable<TimeOfDayResult> GetTimeOfDayAll();
	}
}
