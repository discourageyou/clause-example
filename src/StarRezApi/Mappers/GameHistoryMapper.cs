using StarRezApi.Contracts.V1_0.Responses;
using StarRezApi.Data.Entities;

namespace StarRezApi.Mappers;

public class GameHistoryMapper : IGameHistoryMapper
{
    public HistoryEntryResponse ToResponse(GameHistoryEntry entity)
    {
        return new HistoryEntryResponse(
            entity.Id,
            entity.KidNumber,
            entity.KidResponse,
            entity.ExpectedResponse,
            entity.WasValid,
            entity.ValidatedAt
        );
    }

    public IReadOnlyList<HistoryEntryResponse> ToResponseList(IEnumerable<GameHistoryEntry> entities)
    {
        return entities.Select(ToResponse).ToList();
    }
}
