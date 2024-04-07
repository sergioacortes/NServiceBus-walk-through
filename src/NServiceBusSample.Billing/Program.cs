using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, configurationBuilder) => { })
    .ConfigureServices((builderContext, services) => { });
    
var host = builder.Build();

await host.RunAsync();