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
using System.Web.UI.HtmlControls;

namespace RcMap.Web.Utility
{
    /// <summary>
    /// HTML control related static utility methods.
    /// </summary>
    public static class ControlUtility
    {
        #region Public Interface.
        
        /// <summary>
        /// Creates a CSS link using the specified <paramref name="href"/>.
        /// </summary>
        /// <param name="href">The stylesheet href.</param>
        /// <returns>A stylesheet link.</returns>
        public static HtmlLink CreateCssLink(string href) {

            HtmlLink link = new HtmlLink();

            link.Href = href;
            link.Attributes.Add("media", "screen");
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("type", "text/css");

            return link;
        }

        #endregion
    }
}
