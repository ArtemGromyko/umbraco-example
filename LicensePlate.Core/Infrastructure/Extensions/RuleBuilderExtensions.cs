using FluentValidation;

namespace LicensePlate.Core.Infrastructure.Extensions;

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, TProperty> NotEmptyWtihMassage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().WithMessage("The {PropertyName} is requered");
    }
}
