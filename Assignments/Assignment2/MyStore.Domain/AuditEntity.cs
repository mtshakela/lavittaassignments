using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain
{
    public class AuditEntity<TKey> : BaseEntity<TKey>,IAuditEntity
    {
        [Required]
        [Column("CreatedBy", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string CreatedBy { get; set; }
        [Required]
        [Column("CreatedDate", TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Column("UpdatedBy", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string UpdatedBy { get; set; }
        [Required]
        [Column("UpdatedDate", TypeName = "DateTime")]
        public DateTime UpdatedDate { get; set; }
    }
}
