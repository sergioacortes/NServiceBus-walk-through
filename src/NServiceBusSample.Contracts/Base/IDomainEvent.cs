namespace NServiceBusSample.Contracts.Base;

public interface IDomainEvent
{
    
    public Guid Id { get; set; }
    
}