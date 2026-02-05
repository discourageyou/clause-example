using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Contracts.V1_0.Validators;

namespace StarRezApi.Tests;

public class ValidateRequestValidatorTests
{
    private readonly ValidateRequestValidator _sut = new();

    [Fact]
    public void Validate_ValidRequest_ReturnsSuccess()
    {
        var request = new ValidateRequest { KidNumber = 1, KidResponse = "1" };

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
        var request = new ValidateRequest { KidNumber = kidNumber, KidResponse = "Star" };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("KidNumber"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_EmptyKidResponse_ReturnsError(string? kidResponse)
    {
        var request = new ValidateRequest { KidNumber = 1, KidResponse = kidResponse ?? string.Empty };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Contains("KidResponse"));
    }

    [Fact]
    public void Validate_MultipleErrors_ReturnsAllErrors()
    {
        var request = new ValidateRequest { KidNumber = 0, KidResponse = "" };

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
    }
}
