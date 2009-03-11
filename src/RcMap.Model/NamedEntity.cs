// Copyright (C) 2010 Andy Kernahan
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
using RcMap.Model.Utility;

namespace RcMap.Model
{
    /// <summary>
    /// A <see cref="INamedEntity"/> base class. This class is <see langword="abstract"/>.
    /// </summary>
    [Serializable]
    public abstract class NamedEntity : Entity, INamedEntity
    {
        #region Fields.

        private string _name;
        private string _slug;

        #endregion

        #region Public Interface.

        /// <inheritdoc/>        
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if(!String.Equals(value, _name, StringComparison.Ordinal))
                {
                    _name = value;
                    _slug = _name != null ? StringUtility.Slugify(_name) : null;
                }
            }
        }

        /// <inheritdoc/>
        public virtual string Slug
        {
            get { return _slug; }
        }

        #endregion
    }
}
