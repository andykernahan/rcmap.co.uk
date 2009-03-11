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
using System.Collections.Generic;

using RcMap.Model;

namespace RcMap.Data
{
    /// <summary>
    /// Defines a repository for <see cref="RcMap.Model.Club"/> entities.
    /// </summary>
    public interface IClubRepository : ILocationRepository<Club>
    {
        /// <summary>
        /// Finds the clubs in the specifed region that are are in one or more of the specified 
        /// categories.
        /// </summary>
        /// <param name="centre">The search centre.</param>        
        /// <param name="radius">The search radius, in miles.</param>
        /// <param name="categories">The club categories to search.</param>
        /// <returns>The locations that match the criteria.</returns>
        IList<Club> FindNearbyInCategory(GeoPoint centre, int radius, int[] categories);

        /// <summary>
        /// Finds the clubs in that are in one or more of the specified categories.
        /// </summary>
        /// <param name="categories">The club categories to search.</param>
        /// <returns>The locations that match the criteria.</returns>
        IList<Club> FindInCategory(int[] categories);
    }
}
