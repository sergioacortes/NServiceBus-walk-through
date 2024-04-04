using System.Collections;
using NServiceBusSample.Definitions;

namespace NServiceBusSample.Configuration.Behaviors;

public class OpenTelemetryBehaviorHeaderContainer: IEnumerable
{
    private readonly ISet<string> headers;

    private static IEnumerable<string> BusinessPropertiesResolver
    {
        get => ReadValuePropertiesExtensions.GetStaticStringValues<BusinessProperties>();
    }

    public OpenTelemetryBehaviorHeaderContainer(params string[] inputHeaders)
    {
        this.headers = (ISet<string>) new HashSet<string>((IEnumerable<string>) inputHeaders);
        foreach (string str in OpenTelemetryBehaviorHeaderContainer.BusinessPropertiesResolver)
            this.headers.Add(str);
    }

    public IEnumerator<string> GetEnumerator() => this.headers.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}