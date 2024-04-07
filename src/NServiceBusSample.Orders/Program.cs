using NServiceBusSample.Extensions;
using NServiceBusSample.Orders.BackgroundServices;

var builder = WebApplication.CreateBuilder();

builder.Host
    .AddNServicesBus(builder.Services);

builder.Services
    .AddHostedService<OrdersBackgroundService>();

var host = builder.Build();

await host.RunAsync();