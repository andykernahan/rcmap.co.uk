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
    /// Represents a link.
    /// </summary>
    [Serializable]
    public class Link : NamedEntity
    {
        #region Public Interface.

        /// <summary>
        /// Gets the Link entity category type. This field is constant.
        /// </summary>
        public const string CategoryType = "Link";

        /// <summary>
        /// Initialises a new instance of the Link class.
        /// </summary>
        public Link() { }

        /// <summary>
        /// Get or sets the Url of this Link.
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// Get or sets the Description of this Link.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Get or sets the Category for this Link.
        /// </summary>
        public virtual Category Category { get; set; }

        #endregion
    }
}
