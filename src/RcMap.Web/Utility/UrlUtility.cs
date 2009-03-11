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
using RcMap.Model;

namespace RcMap.Web.Utility
{
    /// <summary>
    /// URL generation utility.
    /// </summary>
    public static class UrlUtility
    {
        #region Public Interface.

        /// <summary>
        /// Generates a URL for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The URL for the specifeid entity.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="entity"/> is <see langword="null"/>.
        /// </exception>
        public static string For<T>(T entity) where T : INamedEntity
        {
            if(entity == null)
            {
                throw Error.ArgumentNull("entity");
            }
            return string.Format("/{0}/{1}.aspx", MakeEntityTypeName(entity.GetType()), entity.Slug);
        }

        #endregion

        #region Private Impl.

        private static string MakeEntityTypeName(Type type)
        {
            return type.Name.ToLowerInvariant();
        }

        #endregion
    }
}
