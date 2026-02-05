using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Contracts.V1_0.Validators;

namespace StarRezApi.Tests;

public class HistoryRequestValidatorTests
{
    private readonly HistoryRequestValidator _sut = new();

    [Fact]
    public void Validate_ValidRequest_ReturnsSuccess()
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = 100, Offset = 0 };

        var result = _sut.Validate(request);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Validate_NullKidNumber_ReturnsSuccess()
    {
        var request = new HistoryRequest { KidNumber = null, Limit = 100, Offset = 0 };

        var result = _sut.Validate(request);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_InvalidKidNumber_ReturnsError(int kidNumber)
    {
        var request = new HistoryRequest { KidNumber = kidNumber, Limit = 100, Offset = 0 };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("KidNumber"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_InvalidLimit_ReturnsError(int limit)
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = limit, Offset = 0 };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("Limit"));
    }

    [Fact]
    public void Validate_LimitExceedsMaximum_ReturnsError()
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = 1001, Offset = 0 };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("Limit"));
    }

    [Fact]
    public void Validate_LimitAtMaximum_ReturnsSuccess()
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = 1000, Offset = 0 };

        var result = _sut.Validate(request);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_NegativeOffset_ReturnsError(int offset)
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = 100, Offset = offset };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("Offset"));
    }

    [Fact]
    public void Validate_MultipleErrors_ReturnsAllErrors()
    {
        var request = new HistoryRequest { KidNumber = 0, Limit = 0, Offset = -1 };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Equal(3, result.Errors.Count);
    }
}
