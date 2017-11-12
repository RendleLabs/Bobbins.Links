using System.Threading;
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
        public static async Task<int> IncrementCommentCountAsync(this LinkContext context, int linkId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT increment_comment_count({0})", new object[] {linkId}, ct);
        }
        
        public static async Task<int> AddUpVoteAsync(this LinkContext context, int linkId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT add_up_vote({0})", new object[] {linkId}, ct);
        }
        
        public static async Task<int> AddDownVoteAsync(this LinkContext context, int linkId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT add_down_vote({0})", new object[] {linkId}, ct);
        }
        
        public static async Task<int> ChangeUpVoteAsync(this LinkContext context, int linkId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT change_up_vote({0})", new object[] {linkId}, ct);
        }
        
        public static async Task<int> ChangeDownVoteAsync(this LinkContext context, int linkId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT change_down_vote({0})", new object[] {linkId}, ct);
        }
    }
}