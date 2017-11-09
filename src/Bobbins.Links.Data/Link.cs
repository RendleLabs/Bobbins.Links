using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Bobbins.Links.Data
{
    [PublicAPI]
    public class Link
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Title { get; set; }

        [MaxLength(256)]
        public string Url { get; set; }

        public int UpVoteCount { get; set; }

        public int DownVoteCount { get; set; }

        public int CommentCount { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        [MaxLength(16)]
        public string User { get; set; }

        public List<Vote> Votes { get; set; }
    }
}
