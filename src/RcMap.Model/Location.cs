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
    /// Represents a physical location.
    /// </summary>
    [Serializable]
    public class Location : NamedEntity, IAuditableEntity
    {
        #region Public Interface.

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Get or sets the <see cref="RcMap.Model.Address"/> of this location.
        /// </summary>        
        public virtual Address Address { get; set; }

        /// <summary>
        /// Get or sets the <see cref="RcMap.Model.GeoPoint"/> of this location.
        /// </summary>
        public virtual GeoPoint GeoPoint { get; set; }

        /// <summary>
        /// Gets or sets the date at which this location was modified.
        /// </summary>        
        public virtual DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the user which created this location.
        /// </summary>        
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date at which this location was modified.
        /// </summary>        
        public virtual DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the user which last modified this location. 
        /// </summary>        
        public virtual string ModifiedBy { get; set; }

        #endregion
    }
}
