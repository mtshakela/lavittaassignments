using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Web.Models
{
    public class OrderViewModel
    {
    
        public int Id { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderItemViewModel> Items { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal Total { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal SubTotal { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }
}