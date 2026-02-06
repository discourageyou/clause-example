using StarRezApi.Data.Entities;

namespace StarRezApi.Repositories;

public interface IGameHistoryRepository
{
    Task<GameHistoryEntry> AddAsync(GameHistoryEntry entry, CancellationToken cancellationToken = default);

    Task<GameHistoryEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<GameHistoryEntry>> GetAllAsync(
        int? kidNumber = null,
        int limit = 100,
        int offset = 0,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(int? kidNumber = null, CancellationToken cancellationToken = default);
}
