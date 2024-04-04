using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NServiceBusSample.Configuration.Serializer;

namespace NServiceBusSample.Configuration;

public static class NewtonSoftExtensions
{
    
    public static EndpointConfiguration UseNewtonsoftJsonSerializer(this EndpointConfiguration endpointConfiguration, 
        Action<JsonSerializerSettings>? applyExtraSettingsAction = null)
    {
        
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = (IContractResolver) new NServiceBusJsonContractResolver(),
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            DateParseHandling = DateParseHandling.DateTime,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            TypeNameHandling = TypeNameHandling.All
        };
        
        if (applyExtraSettingsAction != null)
            applyExtraSettingsAction(jsonSerializerSettings);
        
        endpointConfiguration
            .UseSerialization<NewtonsoftJsonSerializer>()
            .Settings(jsonSerializerSettings);
        
        return endpointConfiguration;
    }
}
    