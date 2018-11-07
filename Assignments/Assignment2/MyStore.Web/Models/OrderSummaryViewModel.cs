using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Web.Models
{
    public class OrderSummaryViewModel
    {
        public int ItemCount { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal TotalAmount { get; set; }
    }
}