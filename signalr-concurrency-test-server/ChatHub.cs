using Microsoft.AspNetCore.SignalR;

namespace signalr_concurrency_test;

public class ChatHub : Hub
{
    public static String? ConnectionId { get; private set; }
    
    public override Task OnConnectedAsync()
    {
        ConnectionId = Context.ConnectionId;

        Console.WriteLine(ConnectionId);
        
        return base.OnConnectedAsync();
    }
}