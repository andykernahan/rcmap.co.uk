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
using System.Security.Principal;

namespace RcMap
{
    /// <summary>
    /// Defines a base class for <see cref="RcMap.IIdentityResolver"/>s. This class is abstract.
    /// </summary>
    [Serializable]
    public abstract class IdentityResolver : IIdentityResolver
    {
        #region Public Interface.

        /// <summary>
        /// Gets the identity of the currently authenticated user.
        /// </summary>
        public virtual IIdentity CurrentIdentity {

            get {
                IPrincipal principal = CurrentPrincipal;

                return principal != null ? principal.Identity : null;
            }
        }

        /// <summary>
        /// When overriden in a derived class; gets the identity of the currently authenticated user.
        /// </summary>
        public abstract IPrincipal CurrentPrincipal { get; }

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Returns a value indicating if the specified <paramref name="principal"/> is authenticated.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>True if the specified <paramref name="principal"/> is authenticated, 
        /// otherwise; false.</returns>
        protected bool IsAuthenticated(IPrincipal principal) {

            return principal != null && IsAuthenticated(principal.Identity);
        }

        /// <summary>
        /// Returns a value indicating if the specified <paramref name="identity"/> is authenticated.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns>True if the specified <paramref name="identity"/> is authenticated, 
        /// otherwise; false.</returns>
        protected bool IsAuthenticated(IIdentity identity) {

            return identity != null && identity.IsAuthenticated;
        }

        #endregion
    }
}
