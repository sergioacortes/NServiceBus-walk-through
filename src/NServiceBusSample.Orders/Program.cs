using NServiceBusSample.Extensions;

var builder = WebApplication.CreateBuilder();

var host = builder.Build();

builder.Host.AddNServicesBus(builder.Services);

await host.RunAsync();