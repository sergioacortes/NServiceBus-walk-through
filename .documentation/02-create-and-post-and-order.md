# Create and post and order with LearningTransport

This solution contains the projects used by the NServiceBus walk through sample.

- NServiceBusSample.Orders, this project is the responsible of post new order on the system.
- NServiceBusSample.Sales, this project is responsible of create the order and dispatch the order completed message.
- NServiceBusSample.Billing, this project is responsible of react to the order completed message to process the billing.

## Functional requirements

Every 5 seconds a new order is posted and as a consequence of that order a new sale is registered in the system, finally, a billing has to be generated.

## Technical implementation

- The NServiceBusSample.Orders microservice has a background service that every 5 second create and send a new PlaceOrderCommand to the NServiceBusSample.Sales.
- The NServiceBusSample.Sales microservice handle the command PlacerOrderCommand and process it. Once the order is processed an OrderPlacedEvent is publish.
- THe NServiceBusSample.Billing microservice handle the event OrderPlacedEvent to generate the corresponding billing.

# Configure NServiceBus

The project NServiceBusSample.Extensions has been created to include extensions methods to configure NServiceBus.

The microservice projects have been changed to an ASP.NET Core web application to be able to use the NServiceBus.Extensions.Hosting package.

For this example, LearningTransport is used.

## Extension method

Every microservice has an extension method to configure NServiceBus, this extension method looks like the following code example

```
using FluentValidation;
using NServiceBus;
using NServiceBusSample.Contracts.Base;
using NServiceBusSample.Extensions.Endpoint;
using NServiceBusSample.Extensions.Options;
using NServiceBusSample.Extensions.Routing;
using NServiceBusSample.Extensions.Validators;

namespace NServiceBusSample.Orders.Extensions;

public static class NServiceBusExtensions
{

    public static IHostBuilder AddNServicesBus(this IHostBuilder hostBuilder, IServiceCollection services)
    {

        hostBuilder.UseNServiceBus((context) =>
        {
            
            var options = context.Configuration.GetSection(NServicesBusOptions.DefaultSectionKey)
                .Get<NServicesBusOptions>();

            new NServicesBusOptionsValidator().ValidateAndThrow(options);
            
            var endpointConfiguration = new EndpointConfiguration(options.Name);

            endpointConfiguration.UseSerialization<SystemJsonSerializer>();
            endpointConfiguration
                .DefineAsCommand(typeof(IDomainCommand))
                .DefineAsEvent(typeof(IDomainEvent));
            
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            transport.ConfigureRouting(context);
            
            return endpointConfiguration;
            
        });
        
        return hostBuilder;

    }
    
}
```

Two this are the most important part 

- Define the commands and define the events. We use an extension method implemented in the project NServiceBusSample.Extensions
- Configure the routing

## Command routing

For the routing configuration, the appsettings.json file has to be modified to define at what queue a specify command want to be sent.

For example, the following appsettings.json define that the command PlacerOrderCommand (within the namespace NServiceBusSample.Contracts.Commands) in the assembly NServiceBusSample.Contracts, will be send to the NServiceBusSample.Sales queue 

```
{
    "Logging": {
        "LogLevel": {
            "Default": "Debug",
            "System": "Information",
            "Microsoft": "Information"
        }
    },
    "NServiceBus": {
        "Name": "NServiceBusSample.Orders",
        "Transport": {
            "Routing": [
                {
                    "Type": "NServiceBusSample.Contracts.Commands.PlacerOrderCommand, NServiceBusSample.Contracts",
                    "Endpoint": "NServiceBusSample.Sales"
                }
            ]
        }
    }
}
```

