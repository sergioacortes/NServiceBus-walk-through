using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus.Transport;

namespace NServiceBusSample.Extensions.Routing;

public static class RoutingExtensions
{

    public static TransportExtensions<T> ConfigureRouting<T>(this TransportExtensions<T> transportExtensions, HostBuilderContext hostBuilderContext)
        where T : TransportDefinition
    {
        
        var routing = transportExtensions.Routing();
        var routeEndpoints = hostBuilderContext
            .Configuration
            .GetSection(NServiceBusSampleConstants.NServiceBusRoutingSectionName)
            .Get<NServiceBusTransportRouting>();

        if (routeEndpoints.Routing is null)
        {
            return transportExtensions;
        }
        
        foreach (var routeEndpoint in routeEndpoints.Routing)
        {
            routing.RouteToEndpoint(Type.GetType(routeEndpoint.Type), routeEndpoint.Endpoint);
        }
        
        return transportExtensions;
    }
    
}