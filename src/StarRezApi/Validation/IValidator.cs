namespace StarRezApi.Validation;

public interface IValidator<T>
{
    void Validate(T instance);
}
