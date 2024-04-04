namespace NServiceBusSample.Definitions;

public class BusinessProperties
{
    
    public static string TenantId => nameof (TenantId);

    public static string PartitionClientId => nameof (PartitionClientId);

    public static string Email => nameof (Email);

    public static string UserCorrelationId => nameof (UserCorrelationId);

    /// <summary>The message type.</summary>
    public static string MessageType => nameof (MessageType);

    
}