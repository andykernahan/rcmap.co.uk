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
    /// Defines a repository for <see cref="RcMap.Model.Location"/> entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface ILocationRepository<T> : INamedEntityRepository<T> where T : Location
    {
        /// <summary>
        /// Finds the locations contained within specified <paramref name="radius"/> around the
        /// specified centre.
        /// </summary>
        /// <param name="centre">The search centre.</param>
        /// <param name="radius">The search radius, in miles.</param>
        /// <returns>The locations contained within the specified geographical search.</returns>
        IList<T> FindNearby(GeoPoint centre, int radius);

        /// <summary>
        /// Finds the locations contained within the specified bounds.
        /// </summary>
        /// <param name="sw">The south-west corner of the bounds.</param>
        /// <param name="ne">The north-east corner of the bounds.</param>        
        /// <returns>The locations contained within the specified geographical bounds.</returns>
        IList<T> FindInBounds(GeoPoint sw, GeoPoint ne);

        /// <summary>
        /// Finds the locations in the specifed country.
        /// </summary>
        /// <param name="countryId">The country Id.</param>
        /// <returns>The locations in the specified country.</returns>
        IList<T> FindByCountry(int countryId);

        /// <summary>
        /// Finds the locations in the specifed region.
        /// </summary>
        /// <param name="regionId">The region Id.</param>
        /// <returns>The locations in the specified country.</returns>
        IList<T> FindByRegion(int regionId);        
    }
}
