using System.Linq.Expressions;
using FluentValidation;
using NServiceBusSample.Extensions.Options;

namespace NServiceBusSample.Extensions.Validators;

public class NServicesBusOptionsValidator : AbstractValidator<NServicesBusOptions>
{
    public NServicesBusOptionsValidator()
    {
        
        this.RuleFor<string>((Expression<Func<NServicesBusOptions, string>>)(x => x.Name))
            .NotEmpty<NServicesBusOptions, string>();
        
    }
}