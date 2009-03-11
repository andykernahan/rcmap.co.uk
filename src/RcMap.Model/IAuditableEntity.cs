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
    /// Defines an auditable entity.
    /// </summary>
    public interface IAuditableEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the date at which the entity was modified.
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the user which created the entity.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date at which the entity was modified.
        /// </summary>
        DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the user which last modified the entity. 
        /// </summary>
        string ModifiedBy { get; set; }
    }
}
