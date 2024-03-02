using Core;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ProducerAPI.Controllers;
[ApiController]
[Route("/Order")]
public class OrderController : ControllerBase
{
    private readonly IBus _bus;
    private readonly IConfiguration _configuration;

    public OrderController(IBus bus, IConfiguration configuration)
    {
        _bus = bus;
        _configuration = configuration;
    }
   
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        var queueName = _configuration
            .GetSection("MassTransit")["QueueName"] ?? string.Empty;
        
        var endpoint = await _bus.GetSendEndpoint(new Uri(queueName));
        
        var uri = new Uri($"queue:{queueName}");
        var endPoint = await _bus.GetSendEndpoint(uri);
        
        Order order = new Order(1,
            
            new User(1, "Fabio", "fabio@email.com"));

        await endPoint.Send(order);
        return Ok();
    }

}