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

using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Expression;
using RcMap.Model;

namespace RcMap.Data
{
    /// <summary>
    /// Provides a non-abstract base repository class for <see cref="RcMap.Model.INamedEntity"/>
    /// entities.
    /// </summary>
    public class NamedEntityRepository<T> : Repository<T>, INamedEntityRepository<T> where T : INamedEntity
    {
        #region Private Fields.

        private static readonly Order[] DEFAULT_ORDERS = new Order[] { Order.Asc("Name") };

        #endregion

        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the NamedEntityRepository class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public NamedEntityRepository(ISessionManager sessionManager) : base(sessionManager) { }

        /// <summary>
        /// Finds the entity with the specified name.
        /// </summary>
        /// <param name="name">The entity name.</param>        
        /// <returns>The entity with the specified name if found, otherwise; null.</returns>
        public T FindByName(string name)
        {
            if(name == null)
                throw Error.ArgumentNull("name");

            return CreateNameCriteria(name).UniqueResult<T>();
        }

        /// <summary>
        /// Finds the entity with the specified slug.
        /// </summary>
        /// <param slug="slug">The entity slug.</param>        
        /// <returns>The entity with the specified slug if found, otherwise; null.</returns>
        public T FindBySlug(string slug)
        {
            if(slug == null)
                throw Error.ArgumentNull("slug");

            return CreateSlugCriteria(slug).UniqueResult<T>();
        }

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Gets the default orders applied to queries with no explit ordering.
        /// </summary>
        /// <returns>The default orders.</returns>
        protected override Order[] GetDefaultOrders()
        {
            return NamedEntityRepository<T>.DEFAULT_ORDERS;
        }

        #endregion

        #region Private Impl.

        private ICriteria CreateNameCriteria(string name)
        {
            return CreateFindCriteria("Name", name);
        }

        private ICriteria CreateSlugCriteria(string name)
        {
            return CreateFindCriteria("Slug", name);
        }

        private ICriteria CreateFindCriteria(string propertyName, string name)
        {
            return CreateCriteria()
                .Add(Expression.Eq(propertyName, name))
                .SetCacheable(true)
                .SetCacheRegion(CacheRegionKey);
        }

        #endregion
    }
}
