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
using System.Diagnostics;

namespace RcMap.Web
{
    /// <summary>
    /// Library error helper.
    /// </summary>
    internal static class Error
    {
        #region Internal Interface.

        internal static ArgumentNullException ArgumentNull(string paramName) {

            return new ArgumentNullException(paramName);
        }

        internal static ArgumentException RcMapClubService_MustSpecifyPointOrCategory() {

            return new ArgumentException(Messages.RcMapClubService_MustSpecifyPointOrCategory);
        }

        internal static ArgumentException RcMapClubService_MustSpecifySeAndNePoint() {

            return new ArgumentException(Messages.RcMapClubService_MustSpecifySeAndNePoint);
        }

        internal static Exception Internal(string message) {

            Debug.Fail(message);
            Trace.Fail(message);

            return new ApplicationException(message);
        }

        #endregion
    }
}
