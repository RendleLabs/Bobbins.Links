using Bobbins.Links.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bobbins.Links.Migrate
{
    public class DesignTimeLinkContextFactory : IDesignTimeDbContextFactory<LinkContext>
    {
        private const string LocalPostgres = "Host=localhost;Database=links;Username=bobbins;Password=secretsquirrel";

        public LinkContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LinkContext>()
                .UseNpgsql(LocalPostgres, b => b.MigrationsAssembly(GetType().Assembly.GetName().Name));
            return new LinkContext(builder.Options);
        }
    }
}