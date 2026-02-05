using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Contracts.V1_0.Validators;

namespace StarRezApi.Tests;

public class CollectionRequestValidatorTests
{
    private readonly CollectionRequestValidator _sut = new();

    [Fact]
    public void Validate_ValidRequest_ReturnsSuccess()
    {
        var request = new CollectionRequest { From = 1, To = 100 };

        var result = _sut.Validate(request);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_InvalidFrom_ReturnsError(int from)
    {
        var request = new CollectionRequest { From = from, To = 100 };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("'from'"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_InvalidTo_ReturnsError(int to)
    {
        var request = new CollectionRequest { From = 1, To = to };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("'to'"));
    }

    [Fact]
    public void Validate_FromGreaterThanTo_ReturnsError()
    {
        var request = new CollectionRequest { From = 100, To = 1 };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("less than or equal"));
    }

    [Fact]
    public void Validate_RangeExceedsMaximum_ReturnsError()
    {
        var request = new CollectionRequest { From = 1, To = 20000 };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("Range"));
    }

    [Fact]
    public void Validate_RangeAtMaximum_ReturnsSuccess()
    {
        var request = new CollectionRequest { From = 1, To = 10001 };

        var result = _sut.Validate(request);

        Assert.True(result.IsValid);
    }
}
