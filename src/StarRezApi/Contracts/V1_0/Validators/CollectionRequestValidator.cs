using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Exceptions;
using StarRezApi.Validation;

namespace StarRezApi.Contracts.V1_0.Validators;

public class CollectionRequestValidator : IValidator<CollectionRequest>
{
    private const int MaxRange = 10000;

    public void Validate(CollectionRequest instance)
    {
        var errors = new List<string>();

        if (instance.From < 1)
            errors.Add("'from' must be a positive integer");

        if (instance.To < 1)
            errors.Add("'to' must be a positive integer");

        if (instance.From > instance.To)
            errors.Add("'from' must be less than or equal to 'to'");

        if (instance.To - instance.From > MaxRange)
            errors.Add($"Range cannot exceed {MaxRange} items");

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}
