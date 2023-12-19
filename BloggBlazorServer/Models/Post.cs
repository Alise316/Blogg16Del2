using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BloggBlazorServer.Models
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
        public DateTime Opprettet { get; set; }

        public int BloggId { get; set; }
        public virtual Blogg Blogg { get; set; }

        public int AntallLikes { get; set; }

        public int AntallKommentarer { get; set; }

        public virtual ICollection<Kommentar> Kommentarer { get; set; }
        public virtual ICollection<Tagg> Tagger { get; set; }

        public virtual ICollection<IdentityUser> TaggedUsers { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}