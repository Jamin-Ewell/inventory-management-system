[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
	private readonly ICosmosDbService _cosmosDbService;

	public ProductController(ICosmosDbService cosmosDbService)
	{
		_cosmosDbService = cosmosDbService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] Product product)
	{
		await _cosmosDbService.AddProductAsync(product);
		// Publish event to Service Bus
		await _serviceBusClient.PublishMessageAsync("ProductCreated", product);
		return Ok(product);
	}
}
