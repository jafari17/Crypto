using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChangePrice.Data.Dto
{
    public class AlertAutoDto
    {
        
        public int AlertAutoId { get; set; }

        public string UserId { get; set; }
        
        public int PriceAlert { get; set; }
       
        public int PriceSteps { get; set; }
        
        public bool NotificationActive { get; set; }
        
        public bool isActive { get; set; }
    }
}
