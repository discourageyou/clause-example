using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Contracts.V1_0.Validators;
using StarRezApi.Exceptions;

namespace StarRezApi.Tests;

public class HistoryRequestValidatorTests
{
    private readonly HistoryRequestValidator _sut = new();

    [Fact]
    public void Validate_ValidRequest_DoesNotThrow()
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = 100, Offset = 0 };

        var exception = Record.Exception(() => _sut.Validate(request));

        Assert.Null(exception);
    }

    [Fact]
    public void Validate_NullKidNumber_DoesNotThrow()
    {
        var request = new HistoryRequest { KidNumber = null, Limit = 100, Offset = 0 };

        var exception = Record.Exception(() => _sut.Validate(request));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_InvalidKidNumber_ThrowsValidationException(int kidNumber)
    {
        var request = new HistoryRequest { KidNumber = kidNumber, Limit = 100, Offset = 0 };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("KidNumber"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_InvalidLimit_ThrowsValidationException(int limit)
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = limit, Offset = 0 };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("Limit"));
    }

    [Fact]
    public void Validate_LimitExceedsMaximum_ThrowsValidationException()
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = 1001, Offset = 0 };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("Limit"));
    }

    [Fact]
    public void Validate_LimitAtMaximum_DoesNotThrow()
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = 1000, Offset = 0 };

        var exception = Record.Exception(() => _sut.Validate(request));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_NegativeOffset_ThrowsValidationException(int offset)
    {
        var request = new HistoryRequest { KidNumber = 1, Limit = 100, Offset = offset };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("Offset"));
    }

    [Fact]
    public void Validate_MultipleErrors_ThrowsWithAllErrors()
    {
        var request = new HistoryRequest { KidNumber = 0, Limit = 0, Offset = -1 };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Equal(3, exception.Errors.Count);
    }
}
