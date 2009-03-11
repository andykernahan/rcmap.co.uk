// Copyright (C) 2008 Andy Kernahan
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;

namespace RcMap.Data
{
    /// <summary>
    /// Defines an entity repository.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Creates a new transient entity.
        /// </summary>
        /// <returns>A new entity of type <typeparamref name="T"/>.</returns>
        T CreateEntity();

        /// <summary>
        /// Returns all entities.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        IList<T> FindAll();

        /// <summary>
        /// Returns all entities, sorted by the specified <paramref name="orders"/>.
        /// </summary>
        /// <param name="orders">The order specifications.</param>        
        /// <returns>A list of all entities.</returns>
        IList<T> FindAll(params Order[] orders);

        /// <summary>
        /// Returns a subset of entities.
        /// </summary>
        /// <param name="firstRow">The index of the first row.</param>
        /// <param name="maxRows">The maximum number of rows to return.</param>
        /// <returns>A subset of entities.</returns>
        IList<T> FindAll(int firstRow, int maxRows);

        /// <summary>
        /// Returns a subset of entities.
        /// </summary>
        /// <param name="firstRow">The index of the first row.</param>
        /// <param name="maxRows">The maximum number of rows to return.</param>
        /// <param name="orders">The order specifications.</param>
        /// <returns>A subset of entities.</returns>
        IList<T> FindAll(int firstRow, int maxRows, params Order[] orders);

        /// <summary>
        /// Returns the number of entities in the repository.
        /// </summary>
        /// <returns>A total number of all entities.</returns>
        int GetCount();

        /// <summary>
        /// Creates a new query given the specified query string.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <returns>The query.</returns>
        IQuery CreateQuery(string query);

        /// <summary>
        /// Creates a new criteria container.
        /// </summary>        
        /// <returns>The criteria container.</returns>
        ICriteria CreateCriteria();

        /// <summary>
        /// Creates a new criteria container.
        /// </summary>
        /// <param name="alias">The entity alias.</param>
        /// <returns>The criteria container.</returns>
        ICriteria CreateCriteria(string alias);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>        
        void Delete(T entity);

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>        
        void Save(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>        
        void Update(T entity);

        /// <summary>
        /// Saves or updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save or update.</param>        
        void SaveOrUpdate(T entity);

        /// <summary>
        /// Returns the entity with the specified Id.
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns>The entity with the specified Id, if found, otherwise; null.</returns>
        T FindById(int id);

        /// <summary>
        /// Returns the entity with the specified Id.
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <param name="lockMode">The lock mode of the returned entity.</param>
        /// <returns>The entity with the specified Id, if found, otherwise; null.</returns>
        T FindById(int id, LockMode lockMode);

        /// <summary>
        /// Gets the cache region key for this repository.
        /// </summary>
        string CacheRegionKey { get;}
    }
}
