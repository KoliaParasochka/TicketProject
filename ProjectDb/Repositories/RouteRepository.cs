using Domain.Entities;
using Domain.Interfaces;
using ProjectDb.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDb.Repositories
{
    /// <summary>
    /// This class works with routes in database context.
    /// </summary>
    public sealed class RouteRepository : IRepository<Route>
    {
        private ApplicationDbContext db;    // Database context.

        public RouteRepository(ApplicationDbContext context)
        {
            if (context == null)
            {
                throw new NotImplementedException();
            }
            db = context;
        }

        /// <summary>
        /// Creating Route
        /// </summary>
        /// <param name="item">New Route object</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Route item)
        {
            if (item != null)
            {
                db.Routes.Add(item);
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
            Route route = db.Routes.Find(id);
            if (route != null)
            {
                db.Routes.Remove(route);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets all elements without using include method
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            return await db.Routes.ToListAsync();
        }

        /// <summary>
        /// Getting elements with path
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The object of another model</param>
        /// <returns>The list of elements including path as a value</returns>
        public async Task<IEnumerable<Route>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Route, C>> path)
        {
            return await db.Routes.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting all elements including path as a list
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The objects of another model</param>
        /// <returns>The list of elements including path as a list</returns>
        public async Task<IEnumerable<Route>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Route, ICollection<C>>> path)
        {
            return await db.Routes.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting element with id 
        /// </summary>
        /// <param name="id">The unique value of element</param>
        /// <returns>Element</returns>
        public async Task<Route> GetAsync(int id)
        {
            return await db.Routes.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updating data
        /// </summary>
        /// <param name="item">This is a changed element</param>
        /// <returns>True if item was not as null and data was changed. Else returns false</returns>
        public async Task<bool> UpdateAsync(Route item)
        {
            if (item != null)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return false;
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Route> Find<C>(Expression<Func<Route, C>> path, Func<Route, Boolean> predicate)
        {
            return db.Routes.Include(path).Where(predicate).ToList();
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Route> Find<C>(Expression<Func<Route, ICollection<C>>> path, Func<Route, Boolean> predicate)
        {
            return db.Routes.Include(path).Where(predicate).ToList();
        }
    }
}
