using System.Collections.Generic;
using RestaurantOrder.Domain.Commands;
using RestaurantOrder.Domain.Handlers;
using RestaurantOrder.Domain.Queries;

namespace RestaurantOrder.Domain.Services
{
	public class OrderService : IOrderService
	{
		private readonly IHandler<CreateOrderCommand> _handler;
		private readonly ITimeOfDayQueries _ofDayQueries;

		public OrderService(
			IHandler<CreateOrderCommand> handler,
			ITimeOfDayQueries ofDayQueries)
		{
			_handler = handler;
			_ofDayQueries = ofDayQueries;
		}
		
		public ICommandResult CreateOrder(CreateOrderCommand command)
		{
			
			return _handler.Handle(command);
		}

		public IEnumerable<TimeOfDayResult> GeTimeOfDays()
		{
			return _ofDayQueries.GetTimeOfDayAll();
		}
	}
}
