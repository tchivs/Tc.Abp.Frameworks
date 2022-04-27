using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.EntityFrameworkCore;

public class IdentityServerMigrationsDbContextFactory : IDesignTimeDbContextFactory<IdentityServerMigrationsDbContext>
{
    public IdentityServerMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<IdentityServerMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new IdentityServerMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}