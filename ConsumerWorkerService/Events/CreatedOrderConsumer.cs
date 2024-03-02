using Core;
using MassTransit;

namespace ConsumerWorkerService.Events;

public class CreatedOrderConsumer : IConsumer<Order>
{
    public Task Consume(ConsumeContext<Order> context)
    {
        Console.WriteLine(context.Message);
        return Task.CompletedTask;
    }
}