using StarRezApi.Contracts.V1_0.Responses;
using StarRezApi.Data.Entities;

namespace StarRezApi.Mappers;

public interface IGameHistoryMapper
{
    HistoryEntryResponse ToResponse(GameHistoryEntry entity);

    IReadOnlyList<HistoryEntryResponse> ToResponseList(IEnumerable<GameHistoryEntry> entities);
}
