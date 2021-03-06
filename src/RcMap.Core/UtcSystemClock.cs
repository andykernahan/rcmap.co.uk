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

namespace RcMap
{
    /// <summary>
    /// Represents a UTC system clock.
    /// </summary>
    [Serializable]
    public class UtcSystemClock : ISystemClock
    {
        #region Public Interface.

        /// <summary>
        /// Returns the current UTC time.
        /// </summary>
        /// <returns>The current UTC time.</returns>
        public DateTime GetTime() {

            return DateTime.UtcNow;
        }

        #endregion
    }
}
