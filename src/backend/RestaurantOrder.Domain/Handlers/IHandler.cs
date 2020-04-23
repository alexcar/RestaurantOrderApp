using RestaurantOrder.Domain.Commands;

namespace RestaurantOrder.Domain.Handlers
{
	public interface IHandler<T> where T : ICommand
	{
		ICommandResult Handle(T command);
	}
}
