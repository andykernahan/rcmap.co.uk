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
using System.Globalization;
using System.Web.UI;
using RcMap.Web.Configuration;

namespace RcMap.Web.UI
{
    /// <summary>
    /// Defines the base class for all master pages within the site. This class is abstract.
    /// </summary>
    public abstract class RcMapMasterPage : MasterPage
    {
        #region Private Fields.

        private static RcMapWebConfiguration _configuration;

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Gets the configuration for the site.
        /// </summary>
        protected RcMapWebConfiguration Configuration
        {
            get
            {
                if(_configuration == null)
                    _configuration = RcMapWebConfiguration.GetSetion();
                return _configuration;
            }
        }

        /// <summary>
        /// Gets the current year as an invariant string.
        /// </summary>
        protected string YearAsString
        {
            get { return DateTime.Now.Year.ToString(CultureInfo.InvariantCulture); }
        }

        #endregion
    }
}