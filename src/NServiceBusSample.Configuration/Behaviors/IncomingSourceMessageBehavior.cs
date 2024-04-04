using NServiceBus.Pipeline;

namespace NServiceBusSample.Configuration.Behaviors;

public class IncomingSourceMessageBehavior: Behavior<IIncomingLogicalMessageContext>
{
    public override async Task Invoke(IIncomingLogicalMessageContext context, Func<Task> next)
    {
        
        if (context.Headers.ContainsKey(NServiceBusSampleConstants.InterfaceKeyName))
            context.Extensions.Set<string>(NServiceBusSampleConstants.InterfaceKeyName, context.Headers[NServiceBusSampleConstants.InterfaceKeyName]);
        
        if (context.Headers.ContainsKey(NServiceBusSampleConstants.ProcessKeyName))
            context.Extensions.Set<string>(NServiceBusSampleConstants.ProcessKeyName, context.Headers[NServiceBusSampleConstants.ProcessKeyName]);
        
        if (context.Headers.ContainsKey(NServiceBusSampleConstants.ActionSourceKeyName))
            context.Extensions.Set<string>(NServiceBusSampleConstants.ActionSourceKeyName, context.Headers[NServiceBusSampleConstants.ActionSourceKeyName]);
        
        await next().ConfigureAwait(false);
        
    }
}