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
using System.Data;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Expression;

namespace RcMap.Data
{
    /// <summary>
    /// Provides a non-abstract base data repository class.
    /// </summary>        
    public class Repository<T> : IRepository<T>
    {
        #region Private Fields.

        private readonly ISessionManager _sessionManager;

        private static readonly Order[] EMPTY_ORDERS = { };
        private static readonly string CACHE_REGION_KEY = typeof(T).Name + "Repository";        

        #endregion

        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the Repository class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public Repository(ISessionManager sessionManager) {

            if(sessionManager == null)
                throw Error.ArgumentNull("sessionManager");

            _sessionManager = sessionManager;            
        }

        /// <summary>
        /// Creates a new transient entity.
        /// </summary>
        /// <returns>A new entity of type <typeparamref name="T"/>.</returns>
        public virtual T CreateEntity() {

            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Returns all entities.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        public virtual IList<T> FindAll() {

            return FindAll(GetDefaultOrders());
        }

        /// <summary>
        /// Returns all entities, sorted by the specified <paramref name="orders"/>.
        /// </summary>
        /// <param name="orders">The order specifications.</param>        
        /// <returns>A list of all entities.</returns>
        public virtual IList<T> FindAll(params Order[] orders) {

            ICriteria criteria = CreateCriteria();

            AddOrdersToCriteria(criteria, orders);

            return criteria.List<T>();
        }

        /// <summary>
        /// Returns a subset of entities.
        /// </summary>
        /// <param name="firstRow">The index of the first row.</param>
        /// <param name="maxRows">The maximum number of rows to return.</param>
        /// <returns>A subset of entities.</returns>
        public virtual IList<T> FindAll(int firstRow, int maxRows) {

            return FindAll(firstRow, maxRows, GetDefaultOrders());
        }

        /// <summary>
        /// Returns a subset of entities.
        /// </summary>
        /// <param name="firstRow">The index of the first row.</param>
        /// <param name="maxRows">The maximum number of rows to return.</param>
        /// <param name="orders">The order specifications.</param>
        /// <returns>A subset of entities.</returns>
        public virtual IList<T> FindAll(int firstRow, int maxRows, params Order[] orders) {

            ICriteria criteria = CreateCriteria()
                .SetFirstResult(firstRow)
                .SetMaxResults(maxRows);

            AddOrdersToCriteria(criteria, orders);

            return criteria.List<T>();
        }

        /// <summary>
        /// Returns the number of entities in the repository.
        /// </summary>
        /// <returns>A total number of all entities.</returns>
        public virtual int GetCount() {

            return CreateCriteria()
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();
        }

        /// <summary>
        /// Creates a new query given the specified query string.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <returns>The query.</returns>
        public virtual IQuery CreateQuery(string query) {

            return OpenSession().CreateQuery(query);
        }

        /// <summary>
        /// Creates a new criteria container.
        /// </summary>        
        /// <returns>The criteria container.</returns>
        public virtual ICriteria CreateCriteria() {

            return OpenSession().CreateCriteria(typeof(T));
        }

        /// <summary>
        /// Creates a new criteria container.
        /// </summary>
        /// <param name="alias">The entity alias.</param>
        /// <returns>The criteria container.</returns>
        public virtual ICriteria CreateCriteria(string alias) {

            return OpenSession().CreateCriteria(typeof(T), alias);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>        
        public virtual void Delete(T entity) {

            if(entity == null)
                throw Error.ArgumentNull("entity");

            ISession session = OpenSession();            

            using(ITransaction transaction = BeginTransaction(session)) {
                session.Delete(entity);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>        
        public virtual void Save(T entity) {

            if(entity == null)
                throw Error.ArgumentNull("entity");

            ISession session = OpenSession();
            
            using(ITransaction transaction = BeginTransaction(session)) {
                session.Save(entity);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>        
        public virtual void Update(T entity) {

            if(entity == null)
                throw Error.ArgumentNull("entity");

            ISession session = OpenSession();
            
            using(ITransaction transaction = BeginTransaction(session)) {
                session.Update(entity);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Saves or updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save or update.</param>        
        public virtual void SaveOrUpdate(T entity) {

            if(entity == null)
                throw Error.ArgumentNull("entity");

            ISession session = OpenSession();
            
            using(ITransaction transaction = BeginTransaction(session)) {
                session.SaveOrUpdate(entity);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Returns the entity with the specified Id.
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns>The entity with the specified Id, if found, otherwise; null.</returns>
        public virtual T FindById(int id) {

            return OpenSession().Get<T>(id);
        }

        /// <summary>
        /// Returns the entity with the specified Id.
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <param name="lockMode">The lock mode of the returned entity.</param>
        /// <returns>The entity with the specified Id, if found, otherwise; null.</returns>
        public virtual T FindById(int id, LockMode lockMode) {

            return OpenSession().Get<T>(id, lockMode);
        }

        /// <summary>
        /// Gets the cache region key for this repository.
        /// </summary>
        public virtual string CacheRegionKey {

            get { return Repository<T>.CACHE_REGION_KEY; }
        }

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Returns an open session.
        /// </summary>
        /// <returns>An open session.</returns>
        protected ISession OpenSession() {

            return SessionManager.OpenSession();
        }

        /// <summary>
        /// Adds the specified <paramref name="orders"/> to the specified <paramref name="criteria"/>.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="orders">The orders. Can be null.</param>
        protected void AddOrdersToCriteria(ICriteria criteria, Order[] orders) {

            if(criteria == null)
                throw Error.ArgumentNull("criteria");

            if(orders != null && orders.Length > 0) {
                foreach(Order order in orders)
                    criteria.AddOrder(order);
            }
        }

        /// <summary>
        /// Gets the default orders applied to queries with no explit ordering.
        /// </summary>
        /// <returns>The default orders.</returns>
        protected virtual Order[] GetDefaultOrders() {

            return Repository<T>.EMPTY_ORDERS;
        }

        /// <summary>
        /// Gets the session manager for this repository.
        /// </summary>
        protected ISessionManager SessionManager {

            get { return _sessionManager; }
        }

        #endregion

        #region Private Impl.

        private ITransaction BeginTransaction(ISession session) {

            if(session == null)
                throw Error.ArgumentNull("session");

            bool weAreOwner = true;
            ITransaction transaction = session.Transaction;

            if(transaction == null || transaction.WasCommitted || transaction.WasRolledBack)
                transaction = session.BeginTransaction();
            else if(transaction.IsActive) {
                // If begin has been called; someone else is reponsible.
                weAreOwner = false;
            }
            transaction.Begin();

            return new TransactionDelegate(transaction, weAreOwner);
        }
        
        private sealed class TransactionDelegate : ITransaction
        {
            private readonly bool _isOwner;
            private readonly ITransaction _transaction;

            public TransactionDelegate(ITransaction transaction, bool isOwner) {

                _transaction = transaction;
                _isOwner = isOwner;
            }            

            public void Begin(IsolationLevel isolationLevel) {

                _transaction.Begin(isolationLevel);
            }

            public void Begin() {

                _transaction.Begin();
            }

            public void Commit() {

                if(_isOwner)
                    _transaction.Commit();
            }

            public void Enlist(IDbCommand command) {

                _transaction.Enlist(command);
            }

            public bool IsActive {

                get { return _transaction.IsActive; }
            }

            public void Rollback() {

                if(_isOwner)
                    _transaction.Rollback();
            }

            public bool WasCommitted {

                get { return _transaction.WasCommitted; }
            }

            public bool WasRolledBack {

                get { return _transaction.WasRolledBack; }
            }

            public void Dispose() {

                if(_isOwner)
                    _transaction.Dispose();
            }
        }

        #endregion
    }
}
