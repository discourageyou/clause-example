using StarRezApi.Contracts.V1_0.Responses;
using StarRezApi.Data.Entities;
using StarRezApi.Repositories;

namespace StarRezApi.Services;

public class GameService : IGameService
{
    private const string Star = "Star";
    private const string Rez = "Rez";
    private const string StarRez = "StarRez";

    private readonly IGameHistoryRepository _historyRepository;

    public GameService(IGameHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public string GetExpectedResponse(int kidNumber)
    {
        bool divisibleBy3 = kidNumber % 3 == 0;
        bool divisibleBy5 = kidNumber % 5 == 0;

        if (divisibleBy3 && divisibleBy5)
            return StarRez;

        if (divisibleBy3)
            return Star;

        if (divisibleBy5)
            return Rez;

        return kidNumber.ToString();
    }

    public bool ValidateResponse(int kidNumber, string kidResponse)
    {
        var expected = GetExpectedResponse(kidNumber);
        return string.Equals(expected, kidResponse, StringComparison.Ordinal);
    }

    public IEnumerable<KidResultDto> GetSequence(int from, int to)
    {
        for (int i = from; i <= to; i++)
        {
            yield return new KidResultDto(i, GetExpectedResponse(i));
        }
    }

    public async Task<Guid> RecordValidationAsync(
        int kidNumber,
        string kidResponse,
        string expectedResponse,
        bool wasValid,
        CancellationToken cancellationToken = default)
    {
        var entry = new GameHistoryEntry
        {
            Id = Guid.NewGuid(),
            KidNumber = kidNumber,
            KidResponse = kidResponse,
            ExpectedResponse = expectedResponse,
            WasValid = wasValid,
            ValidatedAt = DateTime.UtcNow
        };

        await _historyRepository.AddAsync(entry, cancellationToken);

        return entry.Id;
    }
}
