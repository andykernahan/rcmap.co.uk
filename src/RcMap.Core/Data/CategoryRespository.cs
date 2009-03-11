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
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Expression;
using RcMap.Model;

namespace RcMap.Data
{
    /// <summary>
    /// Provides a repository for <see cref="RcMap.Model.Category"/> entities.
    /// </summary>
    public class CategoryRepository : NamedEntityRepository<Category>, ICategoryRepository
    {
        #region Private Fields.

        private static readonly Order[] DEFAULT_ORDERS = new Order[] { 
            Order.Asc("Type"), Order.Asc("SortOrder"), Order.Asc("Name") 
        };

        #endregion

        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the CategoryRepository class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public CategoryRepository(ISessionManager sessionManager) : base(sessionManager) { }

        /// <summary>
        /// Finds the categories of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The category type.</param>
        /// <returns>The categories of the specified <paramref name="type"/>.</returns>
        public virtual IList<Category> FindByType(string type) {

            if(type == null)
                Error.ArgumentNull("type");

            return CreateCriteria()
                .Add(Expression.Eq("Type", type))
                .AddOrder(Order.Asc("SortOrder"))
                .AddOrder(Order.Asc("Name"))
                .List<Category>();
        }

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Gets the default orders applied to queries with no explit ordering.
        /// </summary>
        /// <returns>The default orders.</returns>
        protected override Order[] GetDefaultOrders() {

            return CategoryRepository.DEFAULT_ORDERS;
        }

        #endregion
    }
}
