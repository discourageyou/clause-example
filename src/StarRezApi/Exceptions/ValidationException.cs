namespace StarRezApi.Exceptions;

public class ValidationException : Exception
{
    public IReadOnlyList<string> Errors { get; }

    public ValidationException(IEnumerable<string> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors.ToList();
    }

    public ValidationException(params string[] errors)
        : this((IEnumerable<string>)errors)
    {
    }
}
