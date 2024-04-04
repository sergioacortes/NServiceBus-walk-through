using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus.Pipeline;

namespace NServiceBusSample.Configuration.OpenTelemetry;

public class OutgoingMessagePayloadBehavior: Behavior<IOutgoingPhysicalMessageContext>
{
    
    private const string tagName = "MessagePayload";

    public override async Task Invoke(IOutgoingPhysicalMessageContext context, Func<Task> next)
    {
        
        if (Activity.Current != null && context.Builder.GetService<ICaptureBodySelector>().CaptureBodyEnable())
            Activity.Current.AddTag("MessagePayload", Encoding.UTF8.GetString(context.Body.ToArray()));
        
        await next().ConfigureAwait(false);
        
    }
    
}