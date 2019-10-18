using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Basic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Updating item
        /// </summary>
        /// <param name="item">New item</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T item);

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

        /// <summary>
        /// Getting All elements
        /// </summary>
        /// <returns>The list of T</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Getting all elements 
        /// </summary>
        /// <returns>The list of T including path</returns>
        Task<IEnumerable<T>> GetAllAsync<C>(Expression<Func<T, C>> path);

        /// <summary>
        /// Getting all elements 
        /// </summary>
        /// <returns>The list of T including path</returns>
        Task<IEnumerable<T>> GetAllAsync<C>(Expression<Func<T, ICollection<C>>> path);

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        IEnumerable<T> Find<C>(Expression<Func<T, C>> path, Func<T, Boolean> predicate);

        /// <summary>
        /// Finding all elements 
        /// </summary>
        /// <returns>The list of T including path and true with predicate</returns>
        IEnumerable<T> Find<C>(Expression<Func<T, ICollection<C>>> path, Func<T, Boolean> predicate);

        /// <summary>
        /// Getting item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        Task<T> GetAsync(int id);
    }
}
