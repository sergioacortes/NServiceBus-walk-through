namespace NServiceBusSample.Configuration;

public class NServicesBusOptions
{
    public const string DefaultSectionKey = "NServiceBus";

    public string Name { get; set; }

    public int MaximumConcurrencyLevel { get; set; } = 2;

    public string QueueErrorName { get; set; } = "error";

    public int NumberOfRetries { get; set; } = 2;
}