using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Validation;

namespace StarRezApi.Contracts.V1_0.Validators;

public class HistoryRequestValidator : IValidator<HistoryRequest>
{
    private const int MaxLimit = 1000;

    public ValidationResult Validate(HistoryRequest instance)
    {
        var errors = new List<string>();

        if (instance.KidNumber.HasValue && instance.KidNumber.Value < 1)
            errors.Add("KidNumber must be a positive integer");

        if (instance.Limit < 1)
            errors.Add("Limit must be a positive integer");

        if (instance.Limit > MaxLimit)
            errors.Add($"Limit cannot exceed {MaxLimit}");

        if (instance.Offset < 0)
            errors.Add("Offset cannot be negative");

        return errors.Count > 0
            ? ValidationResult.Failure(errors)
            : ValidationResult.Success();
    }
}
