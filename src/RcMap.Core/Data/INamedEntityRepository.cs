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

using RcMap.Model;

namespace RcMap.Data
{
    /// <summary>
    /// Defines a <see cref="RcMap.Model.INamedEntity"/> repository.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface INamedEntityRepository<T> : IRepository<T> where T : INamedEntity
    {
        /// <summary>
        /// Finds the entity with the specified name.
        /// </summary>
        /// <param name="name">The entity name.</param>        
        /// <returns>The entity with the specified name if found, otherwise; null.</returns>
        T FindByName(string name);

        /// <summary>
        /// Finds the entity with the specified slug.
        /// </summary>
        /// <param name="slug">The entity slug.</param>        
        /// <returns>The entity with the specified slug if found, otherwise; null.</returns>
        T FindBySlug(string slug);
    }
}
