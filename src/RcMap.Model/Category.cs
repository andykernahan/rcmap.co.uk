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
    /// Represents a category.
    /// </summary>
    [Serializable]
    public class Category : NamedEntity
    {
        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the Category class.
        /// </summary>
        public Category() { }

        /// <summary>
        /// Initialises a new instance of the Category class.
        /// </summary>
        /// <param name="type">The category type.</param>
        /// <param name="name">The category name.</param>
        public Category(string type, string name)
        {
            Type = type;
            Name = name;
        }

        /// <summary>
        /// Initialises a new instance of the Category class.
        /// </summary>
        /// <param name="type">The category type.</param>
        /// <param name="name">The category name.</param>
        /// <param name="sortOrder">The sort order.</param>
        public Category(string type, string name, int sortOrder)
            : this(type, name)
        {
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Get or sets the type of this category.
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the sort order of this category.
        /// </summary>
        public virtual int SortOrder { get; set; }

        #endregion
    }
}
