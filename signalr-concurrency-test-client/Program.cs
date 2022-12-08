using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using signalr_concurrency_test_client;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();