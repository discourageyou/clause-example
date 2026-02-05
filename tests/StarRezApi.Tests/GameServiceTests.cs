using Moq;
using StarRezApi.Repositories;
using StarRezApi.Services;

namespace StarRezApi.Tests;

public class GameServiceTests
{
    private readonly GameService _sut;
    private readonly Mock<IGameHistoryRepository> _mockRepository;

    public GameServiceTests()
    {
        _mockRepository = new Mock<IGameHistoryRepository>();
        _sut = new GameService(_mockRepository.Object);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(4, "4")]
    [InlineData(7, "7")]
    public void GetExpectedResponse_RegularNumber_ReturnsNumberAsString(int kidNumber, string expected)
    {
        var result = _sut.GetExpectedResponse(kidNumber);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(9)]
    [InlineData(12)]
    public void GetExpectedResponse_DivisibleBy3Only_ReturnsStar(int kidNumber)
    {
        var result = _sut.GetExpectedResponse(kidNumber);
        Assert.Equal("Star", result);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(25)]
    public void GetExpectedResponse_DivisibleBy5Only_ReturnsRez(int kidNumber)
    {
        var result = _sut.GetExpectedResponse(kidNumber);
        Assert.Equal("Rez", result);
    }

    [Theory]
    [InlineData(15)]
    [InlineData(30)]
    [InlineData(45)]
    [InlineData(60)]
    public void GetExpectedResponse_DivisibleBy3And5_ReturnsStarRez(int kidNumber)
    {
        var result = _sut.GetExpectedResponse(kidNumber);
        Assert.Equal("StarRez", result);
    }

    [Fact]
    public void ValidateResponse_CorrectResponse_ReturnsTrue()
    {
        Assert.True(_sut.ValidateResponse(3, "Star"));
        Assert.True(_sut.ValidateResponse(5, "Rez"));
        Assert.True(_sut.ValidateResponse(15, "StarRez"));
        Assert.True(_sut.ValidateResponse(7, "7"));
    }

    [Fact]
    public void ValidateResponse_IncorrectResponse_ReturnsFalse()
    {
        Assert.False(_sut.ValidateResponse(4, "Star"));
        Assert.False(_sut.ValidateResponse(3, "Rez"));
        Assert.False(_sut.ValidateResponse(15, "Star"));
    }

    [Fact]
    public void ValidateResponse_IsCaseSensitive()
    {
        Assert.False(_sut.ValidateResponse(3, "star"));
        Assert.False(_sut.ValidateResponse(5, "REZ"));
    }

    [Fact]
    public void GetSequence_ReturnsCorrectSequence()
    {
        var result = _sut.GetSequence(1, 15).ToList();

        Assert.Equal(15, result.Count);
        Assert.Equal("1", result[0].KidResponse);
        Assert.Equal("2", result[1].KidResponse);
        Assert.Equal("Star", result[2].KidResponse);
        Assert.Equal("4", result[3].KidResponse);
        Assert.Equal("Rez", result[4].KidResponse);
        Assert.Equal("Star", result[5].KidResponse);
        Assert.Equal("StarRez", result[14].KidResponse);
    }

    [Fact]
    public void GetSequence_FromMiddle_ReturnsCorrectRange()
    {
        var result = _sut.GetSequence(10, 12).ToList();

        Assert.Equal(3, result.Count);
        Assert.Equal(10, result[0].KidNumber);
        Assert.Equal("Rez", result[0].KidResponse);
        Assert.Equal(11, result[1].KidNumber);
        Assert.Equal(12, result[2].KidNumber);
        Assert.Equal("Star", result[2].KidResponse);
    }
}
