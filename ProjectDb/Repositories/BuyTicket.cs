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
    public sealed class BuyTicket : IRepository<ChosenTicket>
    {
        private ApplicationDbContext db;

        public BuyTicket(ApplicationDbContext context)
        {
           if(context != null)
           {
                db = context;
           }
           else
           {
               throw new NullReferenceException();
           }
        }


        /// <summary>
        /// Getting All elements
        /// </summary>
        /// <returns>The list of T</returns>
        public async Task<bool> CreateAsync(ChosenTicket item)
        {
            if(item != null)
            {
                db.ChosenTickets.Add(item);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removing item from database
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            ChosenTicket chosenTicket = db.ChosenTickets.Find(id);
            if(chosenTicket != null)
            {
                db.ChosenTickets.Remove(chosenTicket);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IEnumerable<ChosenTicket> Find<C>(System.Linq.Expressions.Expression<Func<ChosenTicket, C>> path, Func<ChosenTicket, bool> predicate)
        {
            return db.ChosenTickets.Where(predicate).ToList();
        }

        public IEnumerable<ChosenTicket> Find<C>(System.Linq.Expressions.Expression<Func<ChosenTicket, ICollection<C>>> path, Func<ChosenTicket, bool> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Getting all elements 
        /// </summary>
        /// <returns>The list of T including path</returns>
        public async Task<IEnumerable<ChosenTicket>> GetAllAsync()
        {
            return await db.ChosenTickets.ToListAsync();
        }

        public Task<IEnumerable<ChosenTicket>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<ChosenTicket, C>> path)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChosenTicket>> GetAllAsync<C>(System.Linq.Expressions.Expression<Func<ChosenTicket, ICollection<C>>> path)
        {
            throw new NotImplementedException();
        }

        public async Task<ChosenTicket> GetAsync(int id)
        {
            return  await db.ChosenTickets.FindAsync(id);
        }

        public Task<bool> UpdateAsync(ChosenTicket item)
        {
            throw new NotImplementedException();
        }
    }
}
