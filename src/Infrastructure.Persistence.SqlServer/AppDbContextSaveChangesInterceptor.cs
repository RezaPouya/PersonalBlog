using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.SqlServer;

public sealed class AppDbContextSaveChangesInterceptor : SaveChangesInterceptor
{


    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            eventData.Context.ChangeTracker.AddCurrentUserData();

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context is not null)
            eventData.Context.ChangeTracker.AddCurrentUserData();

        return base.SavedChanges(eventData, result);
    }
}