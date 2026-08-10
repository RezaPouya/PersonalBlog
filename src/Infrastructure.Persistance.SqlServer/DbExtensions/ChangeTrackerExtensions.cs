using Iau.Bazaar.Domain.Entities.Identities.Users.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PersonalBlog.Domain.Commons.Base;

namespace Infrastructure.Persistance.SqlServer.DbExtensions;

public static class ChangeTrackerExtensions
{
    public static void AddCurrentUserData(this ChangeTracker changeTracker, ICurrentAppUser currentUser)
    {
        var currentUserId = currentUser?.UserId ?? 0;

        if (currentUserId == 0)
            currentUserId = 0;

        foreach (var entry in changeTracker.Entries<IEntityBase>())
        {
            //if (entry.State == EntityState.Added)
            //    entry.Entity.SetCreatedBy(currentUserId);

            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = DateTime.Now;
        }

        foreach (var entry in changeTracker.Entries<ISoftDelete>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.Entity.MarkAsDeleted();
                entry.State = EntityState.Modified;
            }
        }
    }
}