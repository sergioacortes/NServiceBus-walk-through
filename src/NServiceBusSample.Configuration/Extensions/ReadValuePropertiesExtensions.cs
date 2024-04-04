using System.Reflection;

namespace NServiceBusSample.Configuration;

public class ReadValuePropertiesExtensions
{
    public static IEnumerable<string> GetStaticStringValues<T>()
    {
        return ReadValuePropertiesExtensions.GetStaticValues<T, string>();
    }

    public static IEnumerable<TResult> GetStaticValues<T, TResult>()
    {
        return ((IEnumerable<PropertyInfo>) typeof (T)
            .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy))
            .Where<PropertyInfo>((Func<PropertyInfo, bool>) (fi => fi.PropertyType == typeof (TResult)))
            .Select<PropertyInfo, object>((Func<PropertyInfo, object>) (fi => fi.GetValue((object) null)))
            .Cast<TResult>();
    }
}