using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bobbins.Links.Data.IntegrationTests
{
    public class LinkTests
    {
        private const string LocalPostgres = "Host=localhost;Database=links;Username=bobbins;Password=secretsquirrel";
        
        [Fact]
        public async Task IncrementCommentCount_AddsOneToCommentCount()
        {
            var link = new Link
            {
                Title = "IncrementCommentCountTest",
                Url = "self:link",
                User = "bob"
            };

            using (var context = new LinkContext(Options))
            {
                context.Links.Add(link);
                await context.SaveChangesAsync();

                await context.IncrementCommentCountAsync(link.Id);
                
                await context.Entry(link).ReloadAsync();

                Assert.Equal(1, link.CommentCount);
            }
        }
        
        static DbContextOptions<LinkContext> Options =>
            new DbContextOptionsBuilder<LinkContext>().UseNpgsql(LocalPostgres).Options;
    }
}
