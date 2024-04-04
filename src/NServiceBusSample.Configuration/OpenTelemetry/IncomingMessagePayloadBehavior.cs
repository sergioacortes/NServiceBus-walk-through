using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus.Pipeline;

namespace NServiceBusSample.Configuration.OpenTelemetry;

public class IncomingMessagePayloadBehavior: Behavior<IIncomingPhysicalMessageContext>
{

    private const string tagName = "MessagePayload";

    public override async Task Invoke(IIncomingPhysicalMessageContext context, Func<Task> next)
    {
        
        if (Activity.Current != null && context.Builder.GetService<ICaptureBodySelector>().CaptureBodyEnable())
            Activity.Current.AddTag("MessagePayload", Encoding.UTF8.GetString(context.Message.Body.ToArray()));
        
        await next().ConfigureAwait(false);
        
    }
    
}