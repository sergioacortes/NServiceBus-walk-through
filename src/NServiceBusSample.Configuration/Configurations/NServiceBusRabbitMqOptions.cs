namespace NServiceBusSample.Configuration.Configurations;

public class NServiceBusRabbitMqOptions
{
    public const string DefaultSectionKey = "NServiceBus:Transport:RabbitMq";

    public string ConnectionString { get; set; }

    public TransportTransactionMode TransactionMode { get; set; } = TransportTransactionMode.ReceiveOnly;

    public QueueType QueueType { get; set; } = QueueType.Quorum;

    public int PrefetchMultiplier { get; set; } = 3;

    public TimeSpan HeartbeatInterval { get; set; } = TimeSpan.FromMilliseconds(600.0);
}