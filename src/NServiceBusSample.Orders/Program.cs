var builder = WebApplication.CreateBuilder();

var host = builder.Build();

await host.RunAsync();