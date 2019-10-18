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
    /// This class works with trains in database context
    /// </summary>
    public sealed class TrainRepository : IRepository<Train>
    {
        private ApplicationDbContext db;     // Database context

        public TrainRepository(ApplicationDbContext context)
        {
            if (context == null)
            {
                throw new NotImplementedException();
            }
            db = context;
        }

        /// <summary>
        /// Creating Train
        /// </summary>
        /// <param name="item">New Train object</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Train item)
        {
            if (item != null)
            {
                db.Trains.Add(item);
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
            Train train = db.Trains.Find(id);
            if (train != null)
            {
                db.Trains.Remove(train);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Train> Find<C>(System.Linq.Expressions.Expression<Func<Train, C>> path, Func<Train, bool> predicate)
        {
            return db.Trains.Include(path).Where(predicate).ToList();
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Train> Find<C>(System.Linq.Expressions.Expression<Func<Train, ICollection<C>>> path, Func<Train, bool> predicate)
        {
            return db.Trains.Include(path).Where(predicate).ToList();
        }

        /// <summary>
        /// Gets all elements without using include method
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Train>> GetAllAsync()
        {
            return await db.Trains.ToListAsync();
        }

        /// <summary>
        /// Getting elements with path
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The object of another model</param>
        /// <returns>The list of elements including path as a value</returns>
        public async Task<IEnumerable<Train>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Train, C>> path)
        {
            return await db.Trains.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting all elements including path as a list
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The objects of another model</param>
        /// <returns>The list of elements including path as a list</returns>
        public async Task<IEnumerable<Train>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Train, ICollection<C>>> path)
        {
            return await db.Trains.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting element with id 
        /// </summary>
        /// <param name="id">The unique value of element</param>
        /// <returns>Element</returns>
        public async Task<Train> GetAsync(int id)
        {
            return await db.Trains.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updating data
        /// </summary>
        /// <param name="item">This is a changed element</param>
        /// <returns>True if item was not as null and data was changed. Else returns false</returns>
        public async Task<bool> UpdateAsync(Train item)
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
