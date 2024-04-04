using System.Linq.Expressions;
using FluentValidation;

namespace NServiceBusSample.Configuration;

public class NServicesBusOptionsValidator : AbstractValidator<NServicesBusOptions>
{
    public NServicesBusOptionsValidator()
    {
        
        this.RuleFor<string>((Expression<Func<NServicesBusOptions, string>>)(x => x.Name))
            .NotEmpty<NServicesBusOptions, string>();
        
        this.RuleFor<int>((Expression<Func<NServicesBusOptions, int>>) (x => x.MaximumConcurrencyLevel))
            .GreaterThanOrEqualTo<NServicesBusOptions, int>(0);
        
        this.RuleFor<string>((Expression<Func<NServicesBusOptions, string>>) (x => x.QueueErrorName))
            .NotEmpty<NServicesBusOptions, string>();
        
        this.RuleFor<int>((Expression<Func<NServicesBusOptions, int>>) (x => x.NumberOfRetries))
            .GreaterThanOrEqualTo<NServicesBusOptions, int>(0);
    }
}