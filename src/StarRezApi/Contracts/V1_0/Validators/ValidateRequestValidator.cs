using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Validation;

namespace StarRezApi.Contracts.V1_0.Validators;

public class ValidateRequestValidator : IValidator<ValidateRequest>
{
    public ValidationResult Validate(ValidateRequest instance)
    {
        var errors = new List<string>();

        if (instance.KidNumber < 1)
            errors.Add("KidNumber must be a positive integer");

        if (string.IsNullOrWhiteSpace(instance.KidResponse))
            errors.Add("KidResponse is required");

        return errors.Count > 0
            ? ValidationResult.Failure(errors)
            : ValidationResult.Success();
    }
}
