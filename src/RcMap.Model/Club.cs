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
using Iesi.Collections.Generic;

namespace RcMap.Model
{
    /// <summary>
    /// Represents a club.
    /// </summary>
    [Serializable]
    public class Club : Location, ICategorised
    {
        #region Private Fields.

        private ISet<Category> _categories;

        #endregion

        #region Public Interface.

        /// <summary>
        /// Gets the Club entity category type. This field is constant.
        /// </summary>
        public const string CategoryType = "Club";

        /// <summary>
        /// Initialises a new instance of the Club class.
        /// </summary>
        public Club() { }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Get or sets the SiteUrl of this Club.
        /// </summary>
        public virtual string SiteUrl { get; set; }

        /// <summary>
        /// Get or sets the Contact for this Club.
        /// </summary>        
        public virtual Contact Contact { get; set; }

        /// <summary>
        /// Get or sets the club categories for this Shop.
        /// </summary>        
        public virtual ISet<Category> Categories
        {
            get
            {
                if(_categories == null)
                    _categories = new HashedSet<Category>();
                return _categories;
            }
            set { _categories = value; }
        }

        #endregion
    }
}
