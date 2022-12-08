using signalr_concurrency_test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<ChatHub>("/Chat");

app.Run();