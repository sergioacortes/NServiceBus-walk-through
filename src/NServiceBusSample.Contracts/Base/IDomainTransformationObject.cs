namespace NServiceBusSample.Contracts.Base;

public interface IDomainTransformationObject
{
    
    Guid Id { get; set; }
    
    DateTime Version { get; set; }
    
}