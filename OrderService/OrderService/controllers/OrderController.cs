[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
	private readonly ICosmosDbService _cosmosDbService;

	public OrderController(ICosmosDbService cosmosDbService)
	{
		_cosmosDbService = cosmosDbService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateOrder([FromBody] Order order)
	{
		await _cosmosDbService.AddProductAsync(order);
		// Publish event to Service Bus
		await _serviceBusClient.PublishMessageAsync("OrderCreated", order);
		return Ok(order);
	}
}
