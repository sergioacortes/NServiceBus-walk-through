using Newtonsoft.Json.Serialization;

namespace NServiceBusSample.Configuration.Serializer;

public class NServiceBusJsonContractResolver : CamelCasePropertyNamesContractResolver
{

    protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
    {
        JsonDictionaryContract dictionaryContract = base.CreateDictionaryContract(objectType);
        dictionaryContract.DictionaryKeyResolver = (Func<string, string>) (propertyName => propertyName);
        return dictionaryContract;
    }
    
}