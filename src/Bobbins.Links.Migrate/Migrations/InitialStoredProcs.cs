using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Bobbins.Links.Migrate.Migrations
{
    public partial class Initial
    {
        public override IReadOnlyList<MigrationOperation> UpOperations =>
            new List<MigrationOperation>(base.UpOperations.Concat(CreateProcOperations()));

        private static IEnumerable<MigrationOperation> CreateProcOperations()
        {
            yield return new SqlOperation
            {
                Sql = @"CREATE FUNCTION increment_comment_count(id INTEGER) RETURNS VOID
                          AS 'UPDATE ""Links"" SET ""CommentCount"" = ""CommentCount"" + 1 WHERE ""Id"" = id'
                          LANGUAGE SQL;"
            };
        }
    }
}