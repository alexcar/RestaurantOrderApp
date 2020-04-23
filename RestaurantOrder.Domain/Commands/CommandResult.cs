namespace RestaurantOrder.Domain.Commands
{
	public class CommandResult :ICommandResult
	{
		public CommandResult()
		{

		}

		public CommandResult(bool success, string message)
		{
			Success = success;
			Message = message;
		}

		public CommandResult(bool success, string message, string foods)
		{
			Success = success;
			Message = message;
			Foods = foods;
		}
		
		public bool Success { get; set; }
		public string Message { get; set; }
		public string Foods { get; set; }
	}
}
