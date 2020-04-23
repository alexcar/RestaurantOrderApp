using Microsoft.AspNetCore.Mvc;
using RestaurantOrder.Domain.Commands;
using RestaurantOrder.Domain.Services;

namespace RestaurantOrder.Api.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
	    private readonly IOrderService _orderService;

	    public OrderController(IOrderService orderService)
	    {
		    _orderService = orderService;
	    }

	    [HttpGet]
	    public IActionResult Get()
	    {
		    return Ok("Api service started");
	    }
	    
	    [HttpPost]
	    public IActionResult Post([FromBody] CreateOrderCommand command)
	    {
		    var result = _orderService.CreateOrder(command);

		    return Ok(result);
	    }

	    [HttpGet]
	    [Route("getTimeOfDay")]
	    public IActionResult GetTimeOfDay()
	    {
		    var timeOfDays = _orderService.GeTimeOfDays();

		    return Ok(timeOfDays);
	    }

	}
}