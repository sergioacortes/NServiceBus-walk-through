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
            var routing = transport.Routing();
            var routeEndpoints = context.Configuration.GetSection("NServiceBus:Transport").Get<NServiceBusTransportRouting>();

            foreach (var routeEndpoint in routeEndpoints.Routing)
            {
                routing.RouteToEndpoint(Type.GetType(routeEndpoint.Type), routeEndpoint.Endpoint);
            }
            
            return endpointConfiguration;
            
        });
        
        return hostBuilder;

    }
    
}