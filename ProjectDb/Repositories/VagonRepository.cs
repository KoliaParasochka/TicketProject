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
    /// This class works with Vagons in database context
    /// </summary>
    public sealed class VagonRepository : IRepository<Vagon>
    {
        private ApplicationDbContext db;     // Database context

        public VagonRepository(ApplicationDbContext context)
        {
            if (context == null)
            {
                throw new NotImplementedException();
            }
            db = context;
        }

        /// <summary>
        /// Creating Vagon
        /// </summary>
        /// <param name="item">New Vagon object</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Vagon item)
        {
            if (item != null)
            {
                db.Vagons.Add(item);
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
            Vagon vagon = db.Vagons.Find(id);
            if (vagon != null)
            {
                db.Vagons.Remove(vagon);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Vagon> Find<C>(System.Linq.Expressions.Expression<Func<Vagon, C>> path, Func<Vagon, bool> predicate)
        {
            return db.Vagons.Include(path).Where(predicate).ToList();
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Vagon> Find<C>(System.Linq.Expressions.Expression<Func<Vagon, ICollection<C>>> path, Func<Vagon, bool> predicate)
        {
            return db.Vagons.Include(path).Where(predicate).ToList();
        }

        /// <summary>
        /// Gets all elements without using include method
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Vagon>> GetAllAsync()
        {
            return await db.Vagons.ToListAsync();
        }

        /// <summary>
        /// Getting elements with path
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The object of another model</param>
        /// <returns>The list of elements including path as a value</returns>
        public async Task<IEnumerable<Vagon>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Vagon, C>> path)
        {
            return await db.Vagons.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting all elements including path as a list
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The objects of another model</param>
        /// <returns>The list of elements including path as a list</returns>
        public async Task<IEnumerable<Vagon>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Vagon, ICollection<C>>> path)
        {
            return await db.Vagons.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting element with id 
        /// </summary>
        /// <param name="id">The unique value of element</param>
        /// <returns>Element</returns>
        public async Task<Vagon> GetAsync(int id)
        {
            return await db.Vagons.FindAsync(id);
        }

        /// <summary>
        /// Updating data
        /// </summary>
        /// <param name="item">This is a changed element</param>
        /// <returns>True if item was not as null and data was changed. Else returns false</returns>
        public async Task<bool> UpdateAsync(Vagon item)
        {
            if (item != null)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return false;
        }
    }
}
