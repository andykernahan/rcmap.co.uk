﻿// Copyright (C) 2010 Andy Kernahan
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
    /// Defines the base class for all entities within the system. This class is <see langword="abstract"/>.
    /// </summary>
    [Serializable]
    public abstract class Entity : IEntity
    {
        #region Public Interface.

        /// <summary>
        /// Defines the identifer of a transient entity. This field is constant.
        /// </summary>
        public const int TransientId = 0;

        /// <inheritdoc/>        
        public virtual int Id { get; set; }

        #endregion
    }
}
