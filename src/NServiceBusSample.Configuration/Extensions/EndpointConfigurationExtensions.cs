using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NServiceBus.Extensions.Logging;
using NServiceBus.Logging;
using NServiceBusSample.Configuration.Behaviors;

public static class EndpointConfigurationExtensions
{
    
    public static EndpointConfiguration ConfigureLogging(this EndpointConfiguration endpointConfiguration, IConfiguration configuration)
    {
        
        LogManager.UseFactory((NServiceBus.Logging.ILoggerFactory)new ExtensionsLoggerFactory(
        LoggerFactory.Create((Action<ILoggingBuilder>)(lb => lb.AddConfiguration(configuration)))));

        return endpointConfiguration;
        
    }
    
    public static EndpointConfiguration DefineAsCommand(this EndpointConfiguration endpointConfiguration, params Type[] types) 
    { 
      
        endpointConfiguration
          .Conventions()
          .DefiningCommandsAs(EndpointConfigurationExtensions.TypesDefinitionsAreValid(types));
    
        return endpointConfiguration;
        
    } 
    
    public static EndpointConfiguration DefineAsEvent(this EndpointConfiguration endpointConfiguration, params Type[] types) 
    { 
      
      endpointConfiguration
          .Conventions()
          .DefiningEventsAs(EndpointConfigurationExtensions.TypesDefinitionsAreValid(types)); 
      
      return endpointConfiguration;
      
    }

    public static EndpointConfiguration DefineAsMessage(this EndpointConfiguration endpointConfiguration, params Type[] types)
    {
    
        endpointConfiguration
            .Conventions()
            .DefiningMessagesAs(EndpointConfigurationExtensions.TypesDefinitionsAreValid(types));
    
        return endpointConfiguration;
      
    }

    public static EndpointConfiguration RegisterHeadersBehaviors(this EndpointConfiguration endpointConfiguration)
    { 

        endpointConfiguration
            .RegisterComponents((Action<IServiceCollection>)(components => 
                components.ConfigureComponent<IncomingSourceMessageBehavior>(DependencyLifecycle.SingleInstance))
            );

        endpointConfiguration
            .RegisterComponents((Action<IServiceCollection>)(components => 
                components.ConfigureComponent<OutgoingSourceMessageBehavior>(DependencyLifecycle.SingleInstance))
            );

        endpointConfiguration
            .Pipeline
            .Register(typeof(IncomingSourceMessageBehavior), "Add default Newpol headers");

        endpointConfiguration
            .Pipeline
            .Register(typeof(OutgoingSourceMessageBehavior), "Add default Newpol headers");

        return endpointConfiguration; 
      
    }
    
    private static Func<Type, bool> TypesDefinitionsAreValid(Type[] types)
    {
        
        return (Func<Type, bool>)(type => 
            type.Namespace != null && (type.IsInterface && !type.IsGenericType || type.IsClass && !type.IsAbstract) &&
            ((IEnumerable<Type>)types).Any<Type>((Func<Type, bool>)(t => t.IsAssignableFrom(type)))
        ); 
        
    } 
}