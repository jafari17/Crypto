using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ChangePrice.Data.Dto
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(200)]
        public string UserName { get; set; }
        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
