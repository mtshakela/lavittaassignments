using MyStore.Domain;
using MyStore.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Persistance.Repositories
{
    public class GenericRepository<TClass> : IGenericRepository<TClass> where TClass : BaseEntity<int>
    {

        private readonly IApplicationDBContext _database;

        public GenericRepository(IApplicationDBContext database)
        {
            _database = database;
        }

        public IQueryable<TClass> GetAll()
        {
            return _database.Set<TClass>();
        }

        public TClass GetById(int id)
        {
            return _database.Set<TClass>()
                .Single(p => p.Id == id);
        }

        public void Add(TClass entity)
        {
            _database.Set<TClass>().Add(entity);
        }

        public void Remove(TClass entity)
        {
            _database.Set<TClass>().Remove(entity);
        }

        public void Save()
        {
            _database.Save();
        }

       

    }
}
