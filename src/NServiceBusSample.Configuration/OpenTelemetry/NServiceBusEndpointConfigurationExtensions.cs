using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBusSample.Configuration.Behaviors;
using NServiceBusSample.Configuration.Configurations;

namespace NServiceBusSample.Configuration.OpenTelemetry;

public static class NServiceBusEndpointConfigurationExtensions
{

    private const string configSection = "OpenTelemetry:CaptureBody";

    /// <summary>
    /// Captures as telemetry the specified custom headers from incoming message.
    /// </summary>
    /// <param name="endpointConfiguration">The NServiceBus endpoint configuration.</param>
    /// <param name="headersFromMessage">The headers added to telemetry from incoming message.</param>
    /// <returns></returns>
    public static EndpointConfiguration InitializeBehaviorHeaderContainer(
        this EndpointConfiguration endpointConfiguration,
        params string[] headersFromMessage)
    {
        
        endpointConfiguration.RegisterComponents(
            registration: (Action<IServiceCollection>)(registration => 
                registration.AddSingleton<OpenTelemetryBehaviorHeaderContainer>(
                    (Func<IServiceProvider, OpenTelemetryBehaviorHeaderContainer>) (x => 
                        new OpenTelemetryBehaviorHeaderContainer(headersFromMessage)
                    )
                )
            )
        );

        return endpointConfiguration;
        
    }

    public static EndpointConfiguration EnableCaptureMessageBody(this EndpointConfiguration endpointConfiguration, IConfiguration configuration, IServiceCollection serviceCollection)
    {
        
        serviceCollection
            .AddSingleton<CaptureBodyConfig>(
                configuration
                    .GetSection(configSection)
                    .Get<CaptureBodyConfig>() ?? 
                throw new Exception(message: "OpenTelemetry:CaptureBody is not configured")
            );
        
        endpointConfiguration.EnableFeature<CaptureMessageBodyFeature>();
        
        return endpointConfiguration;
    }
}