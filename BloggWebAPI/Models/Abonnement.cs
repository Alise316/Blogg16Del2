using Microsoft.AspNetCore.Identity;

namespace BloggWebAPI.Models
{
    public class Abonnement
    {
        public int AbonnementId { get; set; }
        public string UserId { get; set; }
        public int BloggId { get; set; }

        public virtual IdentityUser User { get; set; }
        public virtual Blogg Blogg { get; set; }
    }
}