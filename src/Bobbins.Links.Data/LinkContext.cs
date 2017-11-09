using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bobbins.Links.Data
{
    [PublicAPI]
    public class LinkContext : DbContext
    {
        public LinkContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }

        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vote>()
                .HasIndex(v => new {v.LinkId, v.User})
                .IsUnique();
        }
    }

    public static class LinkContextExtensions
    {
        public static async Task IncrementCommentCount(this LinkContext context, int linkId)
        {
            await context.Database.ExecuteSqlCommandAsync("SELECT increment_comment_count({0})", linkId);
        }
    }
}