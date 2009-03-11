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
using Castle.Facilities.NHibernateIntegration;

using RcMap.Model;

namespace RcMap.Data
{
    /// <summary>
    /// Provides a repository class for Club entities.
    /// </summary>
    public class ClubRepository : LocationRepository<Club>, IClubRepository
    {
        #region Private Fields.

        private static string _findInCategoryQuery;
        private static string _findNearbyInCategoryQuery;

        #endregion

        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the ClubRepository class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public ClubRepository(ISessionManager sessionManager) : base(sessionManager) { }

        /// <summary>
        /// Finds the clubs in the specifed region that are are in one or more of the specified 
        /// categories.
        /// </summary>
        /// <param name="centre">The search centre.</param>
        /// <param name="radius">The search radius, in miles.</param>
        /// <param name="categories">The club categories to search.</param>
        /// <returns>The locations that match the criteria.</returns>
        public IList<Club> FindNearbyInCategory(GeoPoint centre, int radius, 
            int[] categories) {

            if(centre == null)
                throw Error.ArgumentNull("centre");

            return OpenSession().CreateQuery(ClubRepository.FindNearbyInCategoryQuery)
                .SetDouble("latitude", centre.Latitude)
                .SetDouble("longitude", centre.Longitude)
                .SetInt32("radius", radius)
                .SetParameterList("categories", categories)
                .List<Club>();
        }

        /// <summary>
        /// Finds the clubs in that are in one or more of the specified categories.
        /// </summary>
        /// <param name="categories">The club categories to search.</param>
        /// <returns>The locations that match the criteria.</returns>
        public IList<Club> FindInCategory(int[] categories) {

            return OpenSession().CreateQuery(ClubRepository.FindInCategoryQuery)
                .SetParameterList("categories", categories)
                .List<Club>();
        }

        /// <summary>
        /// Defines the query used to find clubs in the specifed region that are are in one or 
        /// more of the specified categories.
        /// </summary>
        public static string FindNearbyInCategoryQuery {

            get {
                if(_findNearbyInCategoryQuery == null)
                    _findNearbyInCategoryQuery = ClubRepository.FindNearbyQuery + " and exists(from loc.Categories c where c.id in (:categories))";
                return _findNearbyInCategoryQuery;
            }
        }

        /// <summary>
        /// Defines the query used to find clubs in one or more of the specifed categories.
        /// </summary>
        public static string FindInCategoryQuery {

            get {
                if(_findInCategoryQuery == null)
                    _findInCategoryQuery = "from Club loc where exists(from loc.Categories c where c.id in (:categories))";
                return _findInCategoryQuery;
            }
        }

        #endregion
    }
}
