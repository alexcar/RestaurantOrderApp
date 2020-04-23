using System.Collections.Generic;
using RestaurantOrder.Domain.Commands;
using RestaurantOrder.Domain.Queries;

namespace RestaurantOrder.Domain.Services
{
	public interface IOrderService
	{
		ICommandResult CreateOrder(CreateOrderCommand command);
		IEnumerable<TimeOfDayResult> GeTimeOfDays();
	}
}
