using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChangePrice.Model_DataBase
{
    public class AlertAuto
    {
        [Key]
        public int AlertAutoId { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string UserId { get; set; }
        [Required]
        public int PriceAlert { get; set; }
        [Required]
        public int PriceSteps { get; set; }
        [Required]
        public bool NotificationActive { get; set; }
        [Required]
        public bool isActive { get; set; }


        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}
