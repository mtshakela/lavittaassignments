using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Web.Models
{
    public class NewOrderViewModel
    {
       public IEnumerable<ProductViewModel> products { get; set; }
    }
}