
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChangePrice.Models
{
    public class Alert
    {
        [Key]
        public int AlertId { get; set; }
        [Required]
        
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        public DateTime? DateRegisterTime { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string Description { get; set; }
        public DateTime? LastTouchPrice { get; set; }
        public bool? IsCrossedUp { get; set; }
        public decimal? PriceDifference { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsTemproprySuspended { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}