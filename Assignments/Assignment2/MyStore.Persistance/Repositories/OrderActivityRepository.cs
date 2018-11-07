using MyStore.Domain;
using MyStore.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Persistance.Repositories
{
    public class OrderActivityRepository : GenericRepository<OrderActivity>, IOrderActivityRepository
    {
        public OrderActivityRepository(IApplicationDBContext database)
            : base(database) { }
    }
}
