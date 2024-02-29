using Core;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProducerAPI.Controllers
{
    [ApiController]
    [Route("/Order")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult Order()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",

            };
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "fila",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = JsonSerializer.Serialize(
                    new Order(1,
                    new User(1, "Fabio", "fabio@email.com")));

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "fila",
                    basicProperties: null,
                    body: body);

            }

            return Ok();
        }
    }
}
