using System;
using System.Collections.Generic;
using NetGears.Database.Entities;

namespace NetGears.Database.Repositories
{
    public interface IRepository<T>
        where T : class, IEntity

    {
        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add(T entity);

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(T entity);

        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Remove(T entity);
        
        /// <summary>
        /// Get entity with the same id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(long id);

        /// <summary>
        /// Get entity with the same predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Func<T, bool> predicate);

        /// <summary>
        /// Get entities with the same predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(Func<T, bool> predicate);

        /// <summary>
        /// Count the number of existing entities
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Count the number of existing entities which corresponds to the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Func<T, bool> predicate);
    }
}