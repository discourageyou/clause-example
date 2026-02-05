using StarRezApi.Contracts.V1_0.Responses;

namespace StarRezApi.Services;

public interface IGameService
{
    string GetExpectedResponse(int kidNumber);

    bool ValidateResponse(int kidNumber, string kidResponse);

    IEnumerable<KidResultDto> GetSequence(int from, int to);

    Task<Guid> RecordValidationAsync(
        int kidNumber,
        string kidResponse,
        CancellationToken cancellationToken = default);
}
