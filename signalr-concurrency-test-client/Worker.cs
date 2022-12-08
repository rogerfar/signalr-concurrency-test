using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;

namespace signalr_concurrency_test_client;

internal class Worker : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5194/Chat")
            .Build();

        hubConnection.On("Test1",  () =>
        {
            Console.WriteLine("Test 1 start");
            Thread.Sleep(5000);
            Console.WriteLine("Test 1 end");

            return "Test1";
        });

        hubConnection.On("Test2",  () =>
        {
            Console.WriteLine("Test 2 start");
            Thread.Sleep(1000);
            Console.WriteLine("Test 2 end");

            return "Test2";
        });

        hubConnection.On("Test3", async () =>
        {
            Console.WriteLine("Test 3 start");
            await Task.Delay(5000);
            Console.WriteLine("Test 3 end");

            return "Test3";
        });

        hubConnection.On("Test4", async () =>
        {
            Console.WriteLine("Test 4 start");
            await Task.Delay(1000);
            Console.WriteLine("Test 4 end");

            return "Test4";
        });

        await hubConnection.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}