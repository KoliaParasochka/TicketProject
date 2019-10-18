using Domain.Entities;
using Domain.Interfaces;
using ProjectDb.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDb.Repositories
{
    /// <summary>
    /// This is a class which works with stations in database context
    /// </summary>
    public sealed class StationRepository : IRepository<Station>
    {
        private ApplicationDbContext db;    // Database context.

        public StationRepository(ApplicationDbContext context)
        {
            if(context == null)
            {
                throw new NotImplementedException();
            }
            db = context;
        }

        /// <summary>
        /// Creating Station
        /// </summary>
        /// <param name="item">New Station object</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Station item)
        {
            if(item != null)
            {
                db.Stations.Add(item);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removing element
        /// </summary>
        /// <param name="id">The unique value of element</param>
        /// <returns>True if element was existed and removed.
        /// Else returns false.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            Station station = db.Stations.Find(id);
            if(station != null)
            {
                db.Stations.Remove(station);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Station> Find<C>(System.Linq.Expressions.Expression<Func<Station, C>> path, Func<Station, bool> predicate)
        {
            return db.Stations.Include(path).Where(predicate).ToList();
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Station> Find<C>(System.Linq.Expressions.Expression<Func<Station, ICollection<C>>> path, Func<Station, bool> predicate)
        {
            return db.Stations.Include(path).Where(predicate).ToList();
        }

        /// <summary>
        /// Gets all elements without using include method
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Station>> GetAllAsync()
        {
            return await db.Stations.ToListAsync();
        }

        /// <summary>
        /// Getting elements with path
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The object of another model</param>
        /// <returns>The list of elements including path as a value</returns>
        public async Task<IEnumerable<Station>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Station, C>> path)
        {
            return await db.Stations.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting all elements including path as a list
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The objects of another model</param>
        /// <returns>The list of elements including path as a list</returns>
        public async Task<IEnumerable<Station>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Station, ICollection<C>>> path)
        {
            return await db.Stations.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting element with id 
        /// </summary>
        /// <param name="id">The unique value of element</param>
        /// <returns>Element</returns>
        public async Task<Station> GetAsync(int id)
        {
            return await db.Stations.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updating data
        /// </summary>
        /// <param name="item">This is a changed element</param>
        /// <returns>True if item was not as null and data was changed. Else returns false</returns>
        public async Task<bool> UpdateAsync(Station item)
        {
            if(item != null)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return false;
        }
    }
}
