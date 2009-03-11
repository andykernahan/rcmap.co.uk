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
    /// Represents a contact.
    /// </summary>
    [Serializable]
    public class Contact : Entity
    {
        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the Contact class.
        /// </summary>
        public Contact() { }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Get or sets the Email of this Contact.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Get or sets the Telephone of this Contact.
        /// </summary>
        public virtual string Fax { get; set; }

        /// <summary>
        /// Get or sets the Telephone of this Contact.
        /// </summary>
        public virtual string Telephone { get; set; }

        /// <summary>
        /// Gets or sets the Name of this Contact.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the EmailValidatedOn of this Contact.
        /// </summary>
        public virtual DateTime? EmailValidatedOn { get; set; }

        #endregion
    }
}
