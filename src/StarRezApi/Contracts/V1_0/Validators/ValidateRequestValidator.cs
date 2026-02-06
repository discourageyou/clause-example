using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Exceptions;
using StarRezApi.Validation;

namespace StarRezApi.Contracts.V1_0.Validators;

public class ValidateRequestValidator : IValidator<ValidateRequest>
{
    public void Validate(ValidateRequest instance)
    {
        var errors = new List<string>();

        if (instance.KidNumber < 1)
            errors.Add("KidNumber must be a positive integer");

        if (string.IsNullOrWhiteSpace(instance.KidResponse))
            errors.Add("KidResponse is required");

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}
