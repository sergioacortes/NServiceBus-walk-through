using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBusSample.Extensions.Options;
using NServiceBusSample.Extensions.Validators;

namespace NServiceBusSample.Extensions;

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
            
            return endpointConfiguration;
            
        });
        
        return hostBuilder;

    }
    
}