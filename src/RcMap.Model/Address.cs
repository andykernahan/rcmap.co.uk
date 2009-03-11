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
    /// Represents an address.
    /// </summary>
    [Serializable]
    public class Address : Entity
    {
        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the Address class.
        /// </summary>
        public Address() { }

        /// <summary>
        /// Get or sets the extended property of this address.
        /// </summary>
        public virtual string Extended { get; set; }

        /// <summary>
        /// Get or sets the street of this address.
        /// </summary>
        public virtual string Street { get; set; }

        /// <summary>
        /// Get or sets the locality of this address.
        /// </summary>
        public virtual string Locality { get; set; }

        /// <summary>
        /// Get or sets the postcode of this address.
        /// </summary>
        public virtual string Postcode { get; set; }

        /// <summary>
        /// Get or sets the <see cref="RcMap.Model.Region"/> of this address.
        /// </summary>        
        public virtual Region Region { get; set; }

        #endregion
    }
}
