namespace StarRezApi.Validation;

public interface IValidator<T>
{
    ValidationResult Validate(T instance);
}

public record ValidationResult(bool IsValid, IReadOnlyList<string> Errors)
{
    public static ValidationResult Success() => new(true, Array.Empty<string>());

    public static ValidationResult Failure(params string[] errors) => new(false, errors);

    public static ValidationResult Failure(IEnumerable<string> errors) => new(false, errors.ToList());
}
