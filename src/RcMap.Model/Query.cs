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
    /// Represents a query submitted by a user.
    /// </summary>
    [Serializable]
    public class Query : Entity
    {
        #region Public Interface.

        /// <summary>
        /// Gets the Query entity category type. This field is constant.
        /// </summary>
        public const string CategoryType = "Query";

        /// <summary>
        /// Initialises a new instance of the Query class.
        /// </summary>
        public Query() { }

        /// <summary>
        /// Get or sets the UserName of this Query.
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Get or sets the UserName of this Query.
        /// </summary>
        public virtual string UserEmail { get; set; }

        /// <summary>
        /// Get or sets the HostAddress of this Query.
        /// </summary>
        public virtual string HostAddress { get; set; }

        /// <summary>
        /// Get or sets the Text of this Query.
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Get or sets the CreatedOn of this Query.
        /// </summary>
        public virtual DateTime CreatedOn { get; set; }

        /// <summary>
        /// Get or sets the Category for this Query.
        /// </summary>
        public virtual Category Category { get; set; }

        #endregion
    }
}
