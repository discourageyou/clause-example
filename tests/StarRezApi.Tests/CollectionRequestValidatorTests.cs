using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Contracts.V1_0.Validators;
using StarRezApi.Exceptions;

namespace StarRezApi.Tests;

public class CollectionRequestValidatorTests
{
    private readonly CollectionRequestValidator _sut = new();

    [Fact]
    public void Validate_ValidRequest_DoesNotThrow()
    {
        var request = new CollectionRequest { From = 1, To = 100 };

        var exception = Record.Exception(() => _sut.Validate(request));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_InvalidFrom_ThrowsValidationException(int from)
    {
        var request = new CollectionRequest { From = from, To = 100 };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("'from'"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_InvalidTo_ThrowsValidationException(int to)
    {
        var request = new CollectionRequest { From = 1, To = to };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("'to'"));
    }

    [Fact]
    public void Validate_FromGreaterThanTo_ThrowsValidationException()
    {
        var request = new CollectionRequest { From = 100, To = 1 };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("less than or equal"));
    }

    [Fact]
    public void Validate_RangeExceedsMaximum_ThrowsValidationException()
    {
        var request = new CollectionRequest { From = 1, To = 20000 };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("Range"));
    }

    [Fact]
    public void Validate_RangeAtMaximum_DoesNotThrow()
    {
        var request = new CollectionRequest { From = 1, To = 10001 };

        var exception = Record.Exception(() => _sut.Validate(request));

        Assert.Null(exception);
    }
}
