// Copyright 2007-2008 Andy Kernahan
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
using System.Configuration;

namespace RcMap.Web.Configuration
{
    /// <summary>
    /// Represents a collection of 
    /// <see cref="RcMap.Web.Configuration.LocationExporterElement"/>s.
    /// </summary>
    public class LocationExporterElementCollection : ConfigurationElementCollection
    {
        #region Public Interface.

        /// <summary>
        /// Intialises a new instance of the LocationExporterElementCollection class.
        /// </summary>
        public LocationExporterElementCollection() {

            base.AddElementName = "add";
        }

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Creates a new <see cref="RcMap.Web.Configuration.LocationExporterElement"/> 
        /// configuration element.
        /// </summary>
        /// <returns>A new <see cref="RcMap.Web.Configuration.LocationExporterElement"/>  
        /// configuration element.</returns>
        protected override ConfigurationElement CreateNewElement() {

            return new LocationExporterElement();
        }

        /// <summary>
        /// Gets the key for the specified configuration element.
        /// </summary>
        /// <param name="element">The configuration element.</param>
        /// <returns>The key for the specified configuration element.</returns>
        protected override object GetElementKey(ConfigurationElement element) {

            if(element == null)
                throw Error.ArgumentNull("element");

            return ((LocationExporterElement)element).Key;
        }

        /// <summary>
        /// Gets a value indicating if duplicate configurations elements should result in a
        /// <see cref="System.Configuration.ConfigurationException"/> being thrown.
        /// </summary>
        protected override bool ThrowOnDuplicate {

            get { return true; }
        }

        #endregion
    }
}
