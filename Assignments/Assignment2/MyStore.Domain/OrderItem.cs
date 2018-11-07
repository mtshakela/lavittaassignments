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
   public class OrderItem:BaseEntity<int>
    {
        [Required]
        [DefaultValue("1")]
        [Column("Quantity", TypeName = "int")]
        public int Quantity { get; set; }
        [Required]
        [DefaultValue("0")]
        [Column("Price", TypeName = "decimal")]
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
