using Microsoft.AspNetCore.SignalR;

namespace signalr_concurrency_test;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = _serviceProvider.CreateScope();

        var s = scope.ServiceProvider.GetRequiredService<IHubContext<ChatHub>>();

        _ = Task.Run(async () =>
        {
            while (ChatHub.ConnectionId == null) await Task.Delay(10);

            Console.WriteLine("Start Test 1");
            await Task.Delay(1000);

            var result1 = await s.Clients.Client(ChatHub.ConnectionId)
                .InvokeCoreAsync<String>("Test1", Array.Empty<Object?>(), CancellationToken.None);
            Console.WriteLine($"End Test 1 {result1}");
        });

        _ = Task.Run(async () =>
        {
            while (ChatHub.ConnectionId == null) await Task.Delay(10);

            Console.WriteLine("Start Test 2");
            await Task.Delay(2000);

            var result2 = await s.Clients.Client(ChatHub.ConnectionId)
                .InvokeCoreAsync<String>("Test2", Array.Empty<Object?>(), CancellationToken.None);

            Console.WriteLine($"End Test 2 {result2}");
        });

        _ = Task.Run(async () =>
        {
            while (ChatHub.ConnectionId == null) await Task.Delay(10);

            Console.WriteLine("Start Test 3");
            await Task.Delay(10000);

            var result3 = await s.Clients.Client(ChatHub.ConnectionId)
                .InvokeCoreAsync<String>("Test3", Array.Empty<Object?>(), CancellationToken.None);
            Console.WriteLine($"End Test 3 {result3}");
        });

        _ = Task.Run(async () =>
        {
            while (ChatHub.ConnectionId == null) await Task.Delay(10);

            Console.WriteLine("Start Test 4");
            await Task.Delay(12000);

            var result4 = await s.Clients.Client(ChatHub.ConnectionId)
                .InvokeCoreAsync<String>("Test4", Array.Empty<Object?>(), CancellationToken.None);

            Console.WriteLine($"End Test 4 {result4}");
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}