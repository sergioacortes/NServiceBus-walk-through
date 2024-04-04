using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBusSample.Configuration.OpenTelemetry;
using NServiceBusSample.Definitions;

namespace NServiceBusSample.Configuration;

public static class NServiceBusExtensions
{

    public static IHostBuilder AddNServiceBus(this IHostBuilder builder, IServiceCollection serviceCollection)
    {
        
        builder.UseNServiceBus((context) =>
        {
            
            var options = context.Configuration.GetSection(NServicesBusOptions.DefaultSectionKey)
                .Get<NServicesBusOptions>();

            new NServicesBusOptionsValidator().ValidateAndThrow(options);
    
            var endpointConfiguration = new EndpointConfiguration(options.Name)
                .ConfigureLogging(context.Configuration)
                
                //TODO Omar review if this is needed
                // .UseMongoDbPersistence(context.Configuration)
                //TODO Omar review if this is needed
                
                .UseRabbitMqTransport(context.Configuration)
                .DefineAsCommand(typeof(IDomainCommand))
                .DefineAsEvent(typeof(IDomainEvent))
                .DefineAsMessage(typeof(IDomainMessage));
    
            // TODO: Pending to remove. Not in production
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo(options.QueueErrorName);
            endpointConfiguration.LimitMessageProcessingConcurrencyTo(options.MaximumConcurrencyLevel);
            endpointConfiguration.RegisterHeadersBehaviors();
            endpointConfiguration.UseNewtonsoftJsonSerializer();
            endpointConfiguration.CustomDiagnosticsWriter((s, cancellationToken) => Task.CompletedTask);

            endpointConfiguration
                .InitializeBehaviorHeaderContainer("a3InnuvaProcessId", "a3InnuvaSourceAction")
                .EnableCaptureMessageBody(context.Configuration, serviceCollection)
                .EnableOpenTelemetry();
            
            endpointConfiguration.Recoverability()
                .Immediate(settings => settings.NumberOfRetries(options.NumberOfRetries))
                .CustomPolicy(DefaultPolicy.ExponentialDelayedRetry)
                .AddUnrecoverableException<ValidationException>()
                .AddUnrecoverableException<DomainUnrecoverableException>();
            
            return endpointConfiguration;
            
        });
        
        return builder;

    }

}