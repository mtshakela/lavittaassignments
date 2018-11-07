using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Persistance.Repositories.Interfaces
{
    public interface IGenericRepository<TClass>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IQueryable<TClass> GetAll();
        /// <summary>
        /// Gets entity by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TClass GetById(int id);
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TClass entity);
        /// <summary>
        /// Deletes entity by id asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        void Remove(TClass entity);
        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns></returns>
        void Save();
    }
}
