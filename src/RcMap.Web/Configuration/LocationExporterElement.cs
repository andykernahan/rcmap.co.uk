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
    /// Represents a location exporter configuration element.
    /// </summary>
    public class LocationExporterElement : ConfigurationElement
    {
        #region Private Fields.

        private Type _type;

        #endregion

        #region Public Interface.

        /// <summary>
        /// Gets the key of the exporter.
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key {

            get { return (string)this["key"]; }
        }

        /// <summary>
        /// Gets the exporter type name.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string TypeName {

            get { return (string)this["type"]; }            
        }

        /// <summary>
        /// Gets the <see cref="System.Type"/> of the exporter.
        /// </summary>
        public Type ExporterType {

            get {
                if(_type == null)
                    _type = Type.GetType(TypeName, true);
                return _type;
            }
        }

        #endregion
    }
}
