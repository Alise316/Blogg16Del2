

namespace BloggWebAPI.Models
{
    public class Post
    {
        public Post()
        {
            Likes = new HashSet<Like>();
        }

        public int PostId { get; set; }
        public string Tittel { get; set; }
        public string Innhold { get; set; }
        public DateTime Opprettet { get; set; } = DateTime.UtcNow;

        public int BloggId { get; set; }
        public int AntallLikes { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}
