using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain
{
   public class OrderActivity: BaseEntity<int>
    {
        [Required]
        [Column("UserId", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [Required]
        [Column("OrderId", TypeName = "int")]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [Required]
        [Column("OrderStatusId", TypeName = "int")]
        public int OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public virtual OrderStatus OrderStatus { get; set; }
         [Column("CreatedBy", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string CreatedBy { get; set; }
        [Column("CreatedDate", TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }

    }
}
