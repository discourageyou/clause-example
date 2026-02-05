using Microsoft.EntityFrameworkCore;
using StarRezApi.Data.Entities;

namespace StarRezApi.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    public DbSet<GameHistoryEntry> GameHistory => Set<GameHistoryEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GameHistoryEntry>(entity =>
        {
            entity.ToTable("GameHistory");

            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.KidNumber);

            entity.HasIndex(e => e.ValidatedAt);

            entity.Property(e => e.KidResponse)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.ExpectedResponse)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}
