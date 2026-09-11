using Microsoft.EntityFrameworkCore.ChangeTracking;
using PersonalBlog.Domain.Commons.Base;

namespace Infrastructure.Persistence.SqlServer.DbExtensions;

public static class ChangeTrackerExtensions
{
    public static void AddCurrentUserData(this ChangeTracker changeTracker)
    {
        foreach (var entry in changeTracker.Entries<IEntityBase>())
        {
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