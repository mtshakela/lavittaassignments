using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal _price;
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal Price { get {
                return _price;
            } set { _price = (DiscountPercentage > 0) ? value- value * (DiscountPercentage/100) : value; } }
        public string ImageUrl { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal DiscountPercentage { get; set; }
   
        [Required(ErrorMessage = "Pleasee enter you number")]
        [Range(1, 40, ErrorMessage = "Enter number between 0 to 40")]
        public int Quantity { get; set; }
    }
}