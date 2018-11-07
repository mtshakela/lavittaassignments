using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain
{
   public class Order:AuditEntity<int>
    {
     
   
        [Column("UserId", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public int DeliveryMethodId { get; set; }
        [ForeignKey("DeliveryMethodId")]
        public virtual DeliveryMethod DeliveryMethod { get; set; }
        public int? OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public virtual OrderStatus OrderStatus { get; set; }

    
        
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
