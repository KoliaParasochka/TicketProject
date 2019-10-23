using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITicketRepository<T>  where T : class
    {
        /// <summary>
        /// Getting All elements
        /// </summary>
        /// <returns>The list of T</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Adding item to database
        /// </summary>
        /// <param name="item">New item</param>
        /// <returns></returns>
        Task<bool> CreateAsync(T item);

        /// <summary>
        /// Removing item from database
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);
    }
}
