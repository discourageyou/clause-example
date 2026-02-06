using StarRezApi.Contracts.V1_0.Responses;
using StarRezApi.Data.Entities;
using StarRezApi.Services;

namespace StarRezApi.Mappers;

public class GameHistoryMapper : IGameHistoryMapper
{
    private readonly IGameService _gameService;

    public GameHistoryMapper(IGameService gameService)
    {
        _gameService = gameService;
    }

    public HistoryEntryResponse ToResponse(GameHistoryEntry entity)
    {
        var expectedResponse = _gameService.GetExpectedResponse(entity.KidNumber);
        var wasValid = _gameService.ValidateResponse(entity.KidNumber, entity.KidResponse);

        return new HistoryEntryResponse(
            entity.Id,
            entity.KidNumber,
            entity.KidResponse,
            expectedResponse,
            wasValid,
            entity.ValidatedAt
        );
    }

    public IReadOnlyList<HistoryEntryResponse> ToResponseList(IEnumerable<GameHistoryEntry> entities)
    {
        return entities.Select(ToResponse).ToList();
    }
}
