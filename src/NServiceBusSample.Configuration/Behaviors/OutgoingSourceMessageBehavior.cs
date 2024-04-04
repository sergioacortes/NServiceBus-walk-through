using NServiceBus.Pipeline;

namespace NServiceBusSample.Configuration.Behaviors;

public class OutgoingSourceMessageBehavior: Behavior<IOutgoingLogicalMessageContext>
{
    public override async Task Invoke(IOutgoingLogicalMessageContext context, Func<Task> next)
    {
        string interfaceKeyName;
        
        if (context.Extensions.TryGet<string>(NServiceBusSampleConstants.InterfaceKeyName, out interfaceKeyName))
            context.Headers[NServiceBusSampleConstants.InterfaceKeyName] = interfaceKeyName;
        
        string processKeyName;
        
        if (context.Extensions.TryGet<string>(NServiceBusSampleConstants.ProcessKeyName, out processKeyName))
            context.Headers[NServiceBusSampleConstants.ProcessKeyName] = processKeyName;
        
        string actionSourceKeyName;
        
        if (context.Extensions.TryGet<string>(NServiceBusSampleConstants.ActionSourceKeyName, out actionSourceKeyName))
            context.Headers[NServiceBusSampleConstants.ActionSourceKeyName] = actionSourceKeyName;
        
        await next().ConfigureAwait(false);
    }
}