using System.ComponentModel.DataAnnotations;

namespace Boutique_ZR.ViewModel
{
    public class MemberLoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
