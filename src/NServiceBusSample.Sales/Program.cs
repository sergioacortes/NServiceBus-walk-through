var builder = WebApplication.CreateBuilder(args);

var host = builder.Build();

await host.RunAsync();