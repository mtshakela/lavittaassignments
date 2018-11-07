using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain
{
   public class NamedEntity<TKey>:AuditEntity<TKey>
    {
        [Required]
        [Column("Name", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
