using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain
{
    public class DeliveryAddress:BaseEntity<int>
    {
        [Required]
        [Column("RecipientContact", TypeName = "NVARCHAR")]
        [StringLength(15)]
        public string RecipientContact { get; set; }
        [Required]
        [Column("RecipientName", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public string RecipientName { get; set; }
        [Required]
        [Column("StreetAddress", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public string StreetAddress { get; set; }
        [Required]
        [Column("UserId", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public string UserId { get; set; }
    }
}
