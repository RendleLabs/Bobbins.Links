namespace Bobbins.Links.Models
{
    public class VoteDto
    {
        public int Id { get; set; }

        public int LinkId { get; set; }

        public int Value { get; set; }

        public string User { get; set; }
    }
}