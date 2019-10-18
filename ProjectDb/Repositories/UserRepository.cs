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
    /// This class works with routes in database context.
    /// </summary>
    public sealed class UserRepository : IRepository<MyUser>
    {
        private ApplicationDbContext db;    // Database context.

        public UserRepository(ApplicationDbContext context)
        {
            if (context != null)
            {
                db = context;
            }
            else
            {
                throw new NullReferenceException();
            }
            
        }

        /// <summary>
        /// Creating User
        /// </summary>
        /// <param name="item">New Route object</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(MyUser item)
        {
            if (item != null)
            {
                db.ApplicationUsers.Add(item);
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
            MyUser applicationUser = db.ApplicationUsers.Find(id);
            if (applicationUser != null)
            {
                db.ApplicationUsers.Remove(applicationUser);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets all elements without using include method
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MyUser>> GetAllAsync()
        {
            return await db.ApplicationUsers.ToListAsync();
        }

        /// <summary>
        /// Getting elements with path
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The object of another model</param>
        /// <returns>The list of elements including path as a value</returns>
        public async Task<IEnumerable<MyUser>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<MyUser, C>> path)
        {
            return await db.ApplicationUsers.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting all elements including path as a list
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The objects of another model</param>
        /// <returns>The list of elements including path as a list</returns>
        public async Task<IEnumerable<MyUser>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<MyUser, ICollection<C>>> path)
        {
            return await db.ApplicationUsers.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting element with id 
        /// </summary>
        /// <param name="id">The unique value of element</param>
        /// <returns>Element</returns>
        public async Task<MyUser> GetAsync(int id)
        {
            return await db.ApplicationUsers.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updating data
        /// </summary>
        /// <param name="item">This is a changed element</param>
        /// <returns>True if item was not as null and data was changed. Else returns false</returns>
        public async Task<bool> UpdateAsync(MyUser item)
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
