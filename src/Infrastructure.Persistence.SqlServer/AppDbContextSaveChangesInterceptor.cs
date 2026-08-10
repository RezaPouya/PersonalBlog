using Iau.Bazaar.Domain.Entities.Identities.Users.Services;
using Infrastructure.Persistence.SqlServer.DbExtensions;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.SqlServer;

public sealed class AppDbContextSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentAppUser _currentUserService;

    public AppDbContextSaveChangesInterceptor(ICurrentAppUser currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            eventData.Context.ChangeTracker.AddCurrentUserData(_currentUserService);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context is not null)
            eventData.Context.ChangeTracker.AddCurrentUserData(_currentUserService);

        return base.SavedChanges(eventData, result);
    }
}