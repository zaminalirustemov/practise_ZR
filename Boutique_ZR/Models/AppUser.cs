using Microsoft.AspNetCore.Identity;

namespace Boutique_ZR.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        
    }
}
