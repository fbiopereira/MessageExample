using ConsumerWorkerService;
using ConsumerWorkerService.Events;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        var queue = configuration.GetSection("MassTransit")["QueueName"] ?? string.Empty;
        var server = configuration.GetSection("MassTransit")["Server"] ?? string.Empty;
        var userName = configuration.GetSection("MassTransit")["Username"] ?? string.Empty;
        var password = configuration.GetSection("MassTransit")["Password"] ?? string.Empty;

        services.AddHostedService<Worker>();

        services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(server, "/", h =>
                    {
                        h.Username(userName);
                        h.Password(password);
                    });

                    cfg.ReceiveEndpoint(queue, e =>
                    {
                        e.Consumer<CreatedOrderConsumer>();
                    });
                    
                    x.AddConsumer<CreatedOrderConsumer>();
                });
            }
        );
    })
    .Build();

host.Run();