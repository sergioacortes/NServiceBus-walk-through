using NServiceBusSample.Extensions;
using NServiceBusSample.Orders.BackgroundServices;
using NServiceBusSample.Orders.Extensions;

var builder = WebApplication.CreateBuilder();

builder.Host
    .AddNServicesBus(builder.Services);

builder.Services
    .AddHostedService<OrdersBackgroundService>();

var host = builder.Build();

await host.RunAsync();