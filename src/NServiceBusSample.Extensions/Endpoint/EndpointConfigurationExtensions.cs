namespace NServiceBusSample.Extensions.Endpoint;

public static class EndpointConfigurationExtensions
{
    
    public static EndpointConfiguration DefineAsCommand(this EndpointConfiguration endpointConfiguration, params Type[] types)
    {
        endpointConfiguration.Conventions().DefiningCommandsAs(EndpointConfigurationExtensions.TypesDefinitionsAreValid(types));
        return endpointConfiguration;
    }

    public static EndpointConfiguration DefineAsEvent(this EndpointConfiguration endpointConfiguration, params Type[] types)
    {
        endpointConfiguration.Conventions().DefiningEventsAs(EndpointConfigurationExtensions.TypesDefinitionsAreValid(types));
        return endpointConfiguration;
    }

    public static EndpointConfiguration DefineAsMessage(this EndpointConfiguration endpointConfiguration, params Type[] types)
    {
        endpointConfiguration.Conventions().DefiningMessagesAs(EndpointConfigurationExtensions.TypesDefinitionsAreValid(types));
        return endpointConfiguration;
    }
    
    private static Func<Type, bool> TypesDefinitionsAreValid(Type[] types)
    {
        return (Func<Type, bool>) (type => 
            type.Namespace != null && 
            (type.IsInterface && !type.IsGenericType || type.IsClass && !type.IsAbstract) && 
            ((IEnumerable<Type>) types).Any<Type>((Func<Type, bool>) (t => t.IsAssignableFrom(type)))
        );
    }
    
}