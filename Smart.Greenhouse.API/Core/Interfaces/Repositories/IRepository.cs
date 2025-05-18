using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Smart.Greenhouse.API.Core.Interfaces.Repositories
{
    /// <summary>
    /// Generic repository interface for CRUD operations
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        Task<T> GetByIdAsync(int id);
        
        /// <summary>
        /// Get all entities
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// Find entities by condition
        /// </summary>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Add entity
        /// </summary>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Update entity
        /// </summary>
        Task UpdateAsync(T entity);
        
        /// <summary>
        /// Delete entity
        /// </summary>
        Task DeleteAsync(T entity);
        
        /// <summary>
        /// Delete entity by id
        /// </summary>
        Task DeleteByIdAsync(int id);
        
        /// <summary>
        /// Check if entity exists
        /// </summary>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
} 