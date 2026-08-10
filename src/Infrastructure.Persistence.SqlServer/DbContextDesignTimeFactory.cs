global using Infrastructure.Persistence.SqlServer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;

namespace Iau.Bazaar.Infrastructure.SqlStore;

public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public DbContextDesignTimeFactory()
    {
    }

    public AppDbContext CreateDbContext(string[] args)
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>();

        string connectionString = "Data Source=.;Initial Catalog=RpBlogDb;Trusted_Connection=True;TrustServerCertificate=True";

        opts.UseSqlServer(connectionString, b => { b.MigrationsAssembly(typeof(DependencyInjectionExtensions).Assembly.FullName); });

        return new AppDbContext(opts.Options);
    }
}