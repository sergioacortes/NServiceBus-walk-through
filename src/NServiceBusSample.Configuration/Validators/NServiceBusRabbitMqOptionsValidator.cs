using System.Linq.Expressions;
using FluentValidation;
using NServiceBusSample.Configuration.Configurations;

namespace NServiceBusSample.Configuration.Validators;

public class NServiceBusRabbitMqOptionsValidator : AbstractValidator<NServiceBusRabbitMqOptions>
{
    public NServiceBusRabbitMqOptionsValidator()
    {
        
        this.RuleFor<string>((Expression<Func<NServiceBusRabbitMqOptions, string>>)(x => x.ConnectionString))
            .NotEmpty<NServiceBusRabbitMqOptions, string>();
        
        this.RuleFor<int>((Expression<Func<NServiceBusRabbitMqOptions, int>>) (x => x.PrefetchMultiplier))
            .GreaterThan<NServiceBusRabbitMqOptions, int>(0);
        
    }
}