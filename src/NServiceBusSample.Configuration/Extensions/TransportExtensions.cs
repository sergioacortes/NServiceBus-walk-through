using FluentValidation;
using Microsoft.Extensions.Configuration;
using NServiceBusSample.Configuration.Configurations;
using NServiceBusSample.Configuration.Validators;

namespace NServiceBusSample.Configuration;

public static class TransportExtensions
{
    public static EndpointConfiguration UseRabbitMqTransport(
        this EndpointConfiguration endpointConfiguration,
        IConfiguration configuration)
    {
        
        NServiceBusRabbitMqOptions instance = configuration.GetSection("NServiceBus:Transport:RabbitMq").Get<NServiceBusRabbitMqOptions>();
        
        new NServiceBusRabbitMqOptionsValidator().ValidateAndThrow<NServiceBusRabbitMqOptions>(instance);
        
        TransportExtensions<RabbitMQTransport> transportExtensions = RabbitMQTransportSettingsExtensions
            .UseTransport<RabbitMQTransport>(endpointConfiguration)
            .UseConventionalRoutingTopology(instance.QueueType);
        
        transportExtensions.ConnectionString(instance.ConnectionString);
        transportExtensions.Transactions(instance.TransactionMode);
        transportExtensions.PrefetchMultiplier(instance.PrefetchMultiplier);
        transportExtensions.SetHeartbeatInterval(instance.HeartbeatInterval);
        
        return endpointConfiguration;
        
    }
}