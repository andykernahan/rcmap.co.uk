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
using System.Web;
using System.Security.Principal;

namespace RcMap.Web
{
    /// <summary>
    /// Provides the functionality to resolve the identity of the currently authenticated web user.
    /// </summary>
    [Serializable]
    public class WebIdentityResolver : IdentityResolver
    {
        #region Public Interface.

        /// <summary>
        /// Gets the principal of the currently authenticated user.
        /// </summary>
        public override IPrincipal CurrentPrincipal {

            get {
                HttpContext context = HttpContext.Current;

                return context != null && IsAuthenticated(context.User) ? context.User : null;
            }
        }

        #endregion
    }
}
