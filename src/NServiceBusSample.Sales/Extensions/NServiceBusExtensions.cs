using FluentValidation;
using NServiceBus;
using NServiceBusSample.Contracts.Base;
using NServiceBusSample.Extensions.Endpoint;
using NServiceBusSample.Extensions.Options;
using NServiceBusSample.Extensions.Validators;

namespace NServiceBusSample.Sales.Extensions;

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
            endpointConfiguration.UseTransport<LearningTransport>();
            
            endpointConfiguration
                .DefineAsCommand(typeof(IDomainCommand))
                .DefineAsEvent(typeof(IDomainEvent));
            
            return endpointConfiguration;
            
        });
        
        return hostBuilder;

    }
    
}