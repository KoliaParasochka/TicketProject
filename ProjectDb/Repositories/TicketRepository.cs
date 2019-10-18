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
    /// This is a class which works with tickets in database context
    /// </summary>
    public sealed class TicketRepository : IRepository<Ticket>
    {
        private ApplicationDbContext db;    // Database context.

        public TicketRepository(ApplicationDbContext context)
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
        /// Creating Ticket
        /// </summary>
        /// <param name="item">New Station object</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Ticket item)
        {
            if(item != null)
            {
                db.Tickets.Add(item);
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
            Ticket ticket = await db.Tickets.FindAsync(id);
            if(ticket != null)
            {
                db.Tickets.Remove(ticket);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Ticket> Find<C>(System.Linq.Expressions.Expression<Func<Ticket, C>> path, Func<Ticket, bool> predicate)
        {
            return db.Tickets.Include(path).Where(predicate).ToList();
        }

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        public IEnumerable<Ticket> Find<C>(System.Linq.Expressions.Expression<Func<Ticket, ICollection<C>>> path, Func<Ticket, bool> predicate)
        {
            return db.Tickets.Include(path).Where(predicate).ToList();
        }

        /// <summary>
        /// Gets all elements without using include method
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await db.Tickets.ToListAsync();
        }

        /// <summary>
        /// Getting elements with path
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The object of another model</param>
        /// <returns>The list of elements including path as a value</returns>
        public async Task<IEnumerable<Ticket>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Ticket, C>> path)
        {
            return await db.Tickets.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting all elements including path as a list
        /// </summary>
        /// <typeparam name="C">The type of another model</typeparam>
        /// <param name="path">The objects of another model</param>
        /// <returns>The list of elements including path as a list</returns>
        public async Task<IEnumerable<Ticket>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<Ticket, ICollection<C>>> path)
        {
            return await db.Tickets.Include(path).ToListAsync();
        }

        /// <summary>
        /// Getting element with id 
        /// </summary>
        /// <param name="id">The unique value of element</param>
        /// <returns>Element</returns>
        public async Task<Ticket> GetAsync(int id)
        {
            return await db.Tickets.FindAsync(id);
        }

        /// <summary>
        /// Updating data
        /// </summary>
        /// <param name="item">This is a changed element</param>
        /// <returns>True if item was not as null and data was changed. Else returns false</returns>
        public async Task<bool> UpdateAsync(Ticket item)
        {
            if(item != null)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
