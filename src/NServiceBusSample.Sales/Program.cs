using NServiceBusSample.Extensions;
using NServiceBusSample.Sales.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddNServicesBus(builder.Services);

var host = builder.Build();

await host.RunAsync();