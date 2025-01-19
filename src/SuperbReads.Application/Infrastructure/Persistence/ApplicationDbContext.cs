using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SuperbReads.Application.Common;
using SuperbReads.Application.Common.Interfaces;
using SuperbReads.Application.Domain.Entities;

namespace SuperbReads.Application.Infrastructure.Persistence;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ICurrentUserService currentUserService,
    IDomainEventService domainEventService,
    IDateTimeService dateTimeService)
    : DbContext(options)
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            var now = dateTimeService.UtcNow;

#pragma warning disable S1481
            var userId = currentUserService.UserId;
#pragma warning restore S1481

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = null;
                    entry.Entity.UpdatedBy = null;
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = null;
                    entry.Entity.UpdatedAt = now;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                default:
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IHasExternalId>())
        {
            if (entry.State == EntityState.Added && entry.Entity.ExternalId == Guid.Empty)
            {
                entry.Entity.ExternalId = Guid.CreateVersion7();
            }
        }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
            .Select(x => x.Entity.DomainEvents)
            .SelectMany(x => x)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Author>().ToTable("author");
        modelBuilder.Entity<Book>().ToTable("book");

        base.OnModelCreating(modelBuilder);
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await domainEventService.Publish(@event);
        }
    }
}
