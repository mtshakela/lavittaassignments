using MyStore.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Persistance
{
   public interface IApplicationDBContext
    {
        IDbSet<ProductCategory> ProductCategories { get; set; }
        IDbSet<OrderStatus> OrderStatuses { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<OrderItem> OrderItems { get; set; }
        IDbSet<OrderActivity> OrderActivities { get; set; }
        IDbSet<DeliveryMethod> DeliveryMethods { get; set; }
        IDbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        IDbSet<AddressType> AddressTypes { get; set; }
        
        IDbSet<T> Set<T>() where T : class;

        void Save();
    }
}
