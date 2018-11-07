using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain
{
   public class Product:NamedEntity<int>
    {
        [Required]
        [Column("Description", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string Description { get; set; }
        [Required]
        [Column("Price", TypeName = "decimal")]
        public decimal Price { get; set; }
        [Required]
        [Column("ImageUrl", TypeName = "VARCHAR")]
        [StringLength(256)]
        public string ImageUrl { get; set; }
        [Required]
        [Column("DiscountPercentage", TypeName = "decimal")]
        public decimal? DiscountPercentage { get; set; }
        [Required]
        [Column("ProductCategoryId", TypeName = "int")]
        public int ProductCategoryId { get; set; }
        [ForeignKey("ProductCategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
