using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Web.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
    }
}