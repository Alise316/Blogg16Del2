namespace BloggWebAPI.Models
{
    public class Kommentar
    {
        public Kommentar()
        {
            Likes = new HashSet<Like>();
        }

        public int KommentarId { get; set; }
        public string Innhold { get; set; }
        public string ForfatterId { get; set; }

        public int PostId { get; set; }
        public int AntallLikes { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}

