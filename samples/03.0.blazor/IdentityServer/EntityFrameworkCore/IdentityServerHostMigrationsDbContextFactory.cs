using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.EntityFrameworkCore;

public class IdentityServerHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<IdentityServerHostMigrationsDbContext>
{
    public IdentityServerHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<IdentityServerHostMigrationsDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));

        return new IdentityServerHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}