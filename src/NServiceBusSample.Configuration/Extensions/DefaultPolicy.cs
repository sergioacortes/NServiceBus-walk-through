using System.Security.Cryptography;
using NServiceBus.Transport;

namespace NServiceBusSample.Configuration;

public static class DefaultPolicy
{
    
    public static RecoverabilityAction ExponentialDelayedRetry(RecoverabilityConfig config, ErrorContext context)
    {
        
        RecoverabilityAction recoverabilityAction = DefaultRecoverabilityPolicy.Invoke(config, context);
        
        if (!(recoverabilityAction is DelayedRetry))
            return recoverabilityAction;
        
        int int32 = RandomNumberGenerator.GetInt32(0, 3);
        
        return (RecoverabilityAction)RecoverabilityAction.DelayedRetry(
            TimeSpan.FromSeconds(Math.Pow(2.0, (double)context.DelayedDeliveriesPerformed) + (double) int32)
        );
    }
    
}