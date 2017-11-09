using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Bobbins.Links.Data
{
    [PublicAPI]
    public class Vote
    {
        public int Id { get; set; }

        public int LinkId { get; set; }

        public Link Link { get; set; }

        public int Value { get; set; }

        [MaxLength(16)]
        public string User { get; set; }
    }
}