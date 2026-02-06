using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Contracts.V1_0.Validators;
using StarRezApi.Exceptions;

namespace StarRezApi.Tests;

public class ValidateRequestValidatorTests
{
    private readonly ValidateRequestValidator _sut = new();

    [Fact]
    public void Validate_ValidRequest_DoesNotThrow()
    {
        var request = new ValidateRequest { KidNumber = 1, KidResponse = "1" };

        var exception = Record.Exception(() => _sut.Validate(request));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_InvalidKidNumber_ThrowsValidationException(int kidNumber)
    {
        var request = new ValidateRequest { KidNumber = kidNumber, KidResponse = "Star" };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("KidNumber"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_EmptyKidResponse_ThrowsValidationException(string? kidResponse)
    {
        var request = new ValidateRequest { KidNumber = 1, KidResponse = kidResponse ?? string.Empty };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Contains(exception.Errors, e => e.Contains("KidResponse"));
    }

    [Fact]
    public void Validate_MultipleErrors_ThrowsWithAllErrors()
    {
        var request = new ValidateRequest { KidNumber = 0, KidResponse = "" };

        var exception = Assert.Throws<ValidationException>(() => _sut.Validate(request));

        Assert.Equal(2, exception.Errors.Count);
    }
}
