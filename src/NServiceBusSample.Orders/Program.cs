using NServiceBusSample.Extensions;

var builder = WebApplication.CreateBuilder();

builder.Host.AddNServicesBus(builder.Services);

var host = builder.Build();

await host.RunAsync();