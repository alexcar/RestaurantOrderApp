using Flunt.Notifications;
using Flunt.Validations;

namespace RestaurantOrder.Domain.ValueObjects
{
	public class Description : Notifiable
	{
		public Description(string name)
		{
			Name = name;

			AddNotifications(new Contract()
				.Requires()
				.HasMinLen(Name, 2, "Description.Name", "Name must contain at least 2 characters")
				.HasMaxLen(Name, 30, "Description.Name", "Name must contain up to 30 characters")
			);
		}

		public string Name { get; private set; }
	}
}
