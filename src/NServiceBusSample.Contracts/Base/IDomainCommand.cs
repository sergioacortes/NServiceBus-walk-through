namespace NServiceBusSample.Contracts.Base;

public interface IDomainCommand
{
    Guid Id { get; set; }
}