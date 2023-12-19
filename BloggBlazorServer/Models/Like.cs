using Microsoft.AspNetCore.Identity;

namespace BloggBlazorServer.Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public string UserId { get; set; }
        public int? PostId { get; set; }
        public int? KommentarId { get; set; }


        public virtual IdentityUser User { get; set; }
        public virtual Post Post { get; set; }
        public virtual Kommentar Kommentar { get; set; }
    }
}
