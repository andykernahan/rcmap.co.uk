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

namespace RcMap.Model
{
    /// <summary>
    /// Represents a geographical point.
    /// </summary>
    [Serializable]
    public class GeoPoint
    {
        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the GeoPoint class.
        /// </summary>
        public GeoPoint() { }

        /// <summary>
        /// Initialises a new instance of the GeoPoint class.
        /// </summary>
        /// <param name="latitude">The lattitude.</param>
        /// <param name="longitude">The longitude.</param>
        public GeoPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return string.Format("Lat={0}, Lng={1}", Latitude.ToString(),
                Longitude.ToString());
        }

        /// <summary>
        /// Get or sets the Latitude of this GeoPoint.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Get or sets the Longitude of this GeoPoint.
        /// </summary>
        public double Longitude { get; set; }

        #endregion
    }
}
