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
    /// Provides a non-abstract base repository class for Location derived entities.
    /// </summary>
    public class LocationRepository<T> : NamedEntityRepository<T>, ILocationRepository<T> where T: Location
    {
        #region Private Fields.

        private static string _findNearbyQuery;
        private static string _findInBoundsQuery;
        private static string _findByRegionQuery;
        private static string _findByCountryQuery;        

        #endregion

        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the LocationRepository class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public LocationRepository(ISessionManager sessionManager) : base(sessionManager) { }

        /// <summary>
        /// Finds the locations contained within specified <paramref name="radius"/> around the
        /// specified centre.
        /// </summary>
        /// <param name="centre">The search centre.</param>
        /// <param name="radius">The search radius, in miles.</param>
        /// <returns>The locations contained within the specified geographical search.</returns>
        public IList<T> FindNearby(GeoPoint centre, int radius) {

            if(centre == null)
                throw Error.ArgumentNull("centre");

            return CreateQuery(LocationRepository<T>.FindNearbyQuery)
                .SetDouble("latitude", centre.Latitude)
                .SetDouble("longitude", centre.Longitude)
                .SetInt32("radius", radius)
                .List<T>();
        }

        /// <summary>
        /// Finds the locations contained within the specified bounds.
        /// </summary>
        /// <param name="sw">The south-west corner of the bounds.</param>
        /// <param name="ne">The north-east corner of the bounds.</param>
        /// <returns>The locations contained within the specified geographical search.</returns>
        public IList<T> FindInBounds(GeoPoint sw, GeoPoint ne) {

            if(sw == null || ne == null)
                throw Error.ArgumentNull(sw == null ? "sw" : "ne");

            return CreateQuery(LocationRepository<T>.FindInBoundsQuery)
                .SetDouble("seLatitude", sw.Latitude)
                .SetDouble("seLongitude", sw.Longitude)
                .SetDouble("neLatitude", ne.Latitude)
                .SetDouble("neLongitude", ne.Longitude)
                .List<T>();
        }

        /// <summary>
        /// Finds the locations in the specifed country.
        /// </summary>
        /// <param name="countryId">The country Id.</param>
        /// <returns>The locations in the specified country.</returns>
        public IList<T> FindByCountry(int countryId) {

            return CreateQuery(LocationRepository<T>.FindByCountryQuery)
                .SetInt32("countryId", countryId)
                .SetCacheable(true)
                .SetCacheRegion(CacheRegionKey)
                .List<T>();
        }

        /// <summary>
        /// Finds the locations in the specifed region.
        /// </summary>
        /// <param name="regionId">The region Id.</param>
        /// <returns>The locations in the specified country.</returns>
        public IList<T> FindByRegion(int regionId) {

            return CreateQuery(LocationRepository<T>.FindByRegionQuery)
                .SetInt32("regionId", regionId)                
                .SetCacheable(true)
                .SetCacheRegion(CacheRegionKey)
                .List<T>();
        }

        /// <summary>
        /// Defines the query used to find loctions within a specified region.
        /// </summary>
        public static string FindByRegionQuery {

            get {
                if(_findByRegionQuery == null)
                    _findByRegionQuery = string.Format("from {0} loc where loc.Address.Region.id = :regionId", typeof(T).Name);
                return _findByRegionQuery;
            }
        }        

        /// <summary>
        /// Defines the query used to find loctions within a bounds specified by a south-west point and a north-east point.
        /// </summary>
        public static string FindInBoundsQuery {

            get {
                if(_findInBoundsQuery == null)
                    _findInBoundsQuery = string.Format("from {0} loc where (loc.GeoPoint.Latitude >= :swLatitude and loc.GeoPoint.Longitude >= :swLongitude) and (loc.GeoPoint.Latitude <= :neLatitude and loc.GeoPoint.Longitude <= :neLongitude)", typeof(T).Name);
                return _findInBoundsQuery;
            }
        }

        /// <summary>
        /// Defines the query used to find loctions within a specified country.
        /// </summary>
        public static string FindByCountryQuery {

            get {
                if(_findByCountryQuery == null)
                    _findByCountryQuery = string.Format("from {0} loc where loc.Address.Region.Country.id = :countryId", typeof(T).Name);
                return _findByCountryQuery;
            }
        }

        /// <summary>
        /// Defines the query used to find loctions within a specified radius, around a specified point.
        /// </summary>
        public static string FindNearbyQuery {

            get {
                if(_findNearbyQuery == null)
                    _findNearbyQuery = string.Format("from {0} loc where (3959 * acos(cos(radians(:latitude)) * cos(radians(loc.GeoPoint.Latitude)) * cos(radians(loc.GeoPoint.Longitude) - radians(:longitude)) + sin(radians(:latitude)) * sin(radians(loc.GeoPoint.Latitude)))) <= :radius", typeof(T).Name);
                return _findNearbyQuery; 
            }
        }	

        #endregion
    }
}
