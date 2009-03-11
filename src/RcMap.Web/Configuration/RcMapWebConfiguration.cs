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
    /// Provides configuration information for the RC Map site.    
    /// </summary>
    [Serializable]
    public class RcMapWebConfiguration : ConfigurationSection
    {
        #region Public Interface.

        /// <summary>
        /// Helper method to retrieve the RcMapWebConfiguration from its default location.
        /// </summary>
        /// <returns>The RcMapWebConfiguration.</returns>
        public static RcMapWebConfiguration GetSetion() {

            return (RcMapWebConfiguration)ConfigurationManager.GetSection("rcmap.web");
        }

        /// <summary>
        /// Gets the Google Maps API key.
        /// </summary>
        [ConfigurationProperty("mapApiKey", IsRequired = true)]
        public string MapApiKey {

            get { return (string)this["mapApiKey"]; }            
        }

        /// <summary>
        /// Gets the Google Maps Geocode API key.
        /// </summary>
        [ConfigurationProperty("geoApiKey", IsRequired = true)]
        public string GeoApiKey {

            get { return (string)this["geoApiKey"]; }
        }

        /// <summary>
        /// Gets the Google Maps Analytics API key.
        /// </summary>
        [ConfigurationProperty("analyticsKey", IsRequired = true)]
        public string AnalyticsKey {

            get { return (string)this["analyticsKey"]; }
        }

        /// <summary>
        /// Gets a value indicating if analytics is enabled.
        /// </summary>
        [ConfigurationProperty("enableAnalytics", IsRequired = false, DefaultValue = false)]
        public bool EnableAnalytics {

            get { return (bool)this["enableAnalytics"]; }
        }

        /// <summary>
        /// Gets the correspondence email address for the site.
        /// </summary>
        [ConfigurationProperty("emailsTo", IsRequired = true)]
        public string EmailsTo {

            get { return (string)this["emailsTo"]; }
        }

        /// <summary>
        /// Gets the collection of exporters.
        /// </summary>
        [ConfigurationProperty("exporters", IsDefaultCollection = false)]
        public LocationExporterElementCollection Exporters {

            get { return (LocationExporterElementCollection)this["exporters"]; }
        }
       
        #endregion
    }
}
