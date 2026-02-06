using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Exceptions;
using StarRezApi.Validation;

namespace StarRezApi.Contracts.V1_0.Validators;

public class HistoryRequestValidator : IValidator<HistoryRequest>
{
    private const int MaxLimit = 1000;

    public void Validate(HistoryRequest instance)
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

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}
