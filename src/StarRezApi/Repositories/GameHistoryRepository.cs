using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarRezApi.Data;
using StarRezApi.Data.Entities;

namespace StarRezApi.Repositories;

public class GameHistoryRepository : IGameHistoryRepository
{
    private readonly GameDbContext _context;
    private readonly ILogger<GameHistoryRepository> _logger;

    public GameHistoryRepository(GameDbContext context, ILogger<GameHistoryRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GameHistoryEntry> AddAsync(GameHistoryEntry entry, CancellationToken cancellationToken = default)
    {
        _context.GameHistory.Add(entry);
        await _context.SaveChangesAsync(cancellationToken);
        return entry;
    }

    public async Task<GameHistoryEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.GameHistory
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<GameHistoryEntry>> GetAllAsync(
        int? kidNumber = null,
        int limit = 100,
        int offset = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Fetching history entries: kidNumber={KidNumber}, limit={Limit}, offset={Offset}",
            kidNumber, limit, offset);

        var query = _context.GameHistory.AsNoTracking();

        if (kidNumber.HasValue)
        {
            query = query.Where(e => e.KidNumber == kidNumber.Value);
        }

        return await query
            .OrderByDescending(e => e.ValidatedAt)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountAsync(int? kidNumber = null, CancellationToken cancellationToken = default)
    {
        var query = _context.GameHistory.AsQueryable();

        if (kidNumber.HasValue)
        {
            query = query.Where(e => e.KidNumber == kidNumber.Value);
        }

        return await query.CountAsync(cancellationToken);
    }
}
