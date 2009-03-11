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
using AK.Net.Json.Rpc;

using RcMap;
using RcMap.Data;
using RcMap.Model;

namespace RcMap.Web.Services
{
    /// <summary>
    /// RcMap Club Service.
    /// </summary>
    [RpcService(
        "RcMapClubService",
        "9F13363F-2F88-4040-9C2F-3160891572DF",
        "club-service.ashx", 
        Version = "0.1.22.0",
        Summary = "RcMap Club Service",
        Flags = RpcServiceFlags.None)]
    public class RcMapClubService : RcMapService
    {
        #region Private Fields.

        private IClubRepository _clubRepository;        

        #endregion

        #region Public Interface.

        /// <summary>
        /// Finds the clubs contained within the specified geographical search and of the specified categories.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="radius">The search radius, in miles.</param>
        /// <param name="categories">The club categories to search.</param>
        /// <returns>The clubs contained within the specified geographical search.</returns>
        [RpcMethod(
            "Search",
            RpcTypeCode.Array,   
            Summary = "Finds the clubs contained within the specified geographical search and of the specified categories.",
            Flags = RpcMethodFlags.Idempotent)]
        public IList<Club> Search(double latitude, double longitude, int radius, int[] categories) {

            bool hasPoint = latitude != 0D || longitude != 0D;
            bool hasCat = categories != null && categories.Length > 0;

            if(hasPoint && hasCat)
                return ClubRepository.FindNearbyInCategory(new GeoPoint(latitude, longitude), radius, categories);
            if(hasPoint && !hasCat)
                return ClubRepository.FindNearby(new GeoPoint(latitude, longitude), radius);
            if(!hasPoint && hasCat)
                return ClubRepository.FindInCategory(categories);            

            throw Error.RcMapClubService_MustSpecifyPointOrCategory();
        }   

        /// <summary>
        /// Finds the locations contained within the specified bounds.
        /// </summary>
        /// <param name="swLatitude"></param>
        /// <param name="swLongitude"></param>
        /// <param name="neLatitude"></param>
        /// <param name="neLongitude"></param>
        /// <returns>The locations contained within the specified geographical bounds.</returns>
        [RpcMethod(
            "FindInBounds",
            RpcTypeCode.Array,
            Summary = "Finds the locations contained within the specified bounds.",
            Flags = RpcMethodFlags.Idempotent)]
        public IList<Club> FindInBounds(double swLatitude, double swLongitude, double neLatitude, double neLongitude) {

            bool hasSw = swLatitude != 0D || swLongitude != 0D;
            bool hasNe = neLatitude != 0D || neLongitude != 0D;

            if(hasSw && hasNe) {
                return ClubRepository.FindInBounds(new GeoPoint(swLatitude, swLongitude),
                    new GeoPoint(neLatitude, neLongitude));
            }

            throw Error.RcMapClubService_MustSpecifySeAndNePoint();
        }        

        /// <summary>
        /// Finds the clubs in the specifed country.
        /// </summary>
        /// <param name="countryId">The country Id.</param>
        /// <returns>The clubs in the specified country.</returns>
        [RpcMethod(
            "FindByCountry",
            RpcTypeCode.Array,
            Summary = "Finds the clubs in the specifed country.",
            Flags = RpcMethodFlags.Idempotent)]
        public IList<Club> FindByCountry(int countryId) {

            return ClubRepository.FindByCountry(countryId);
        }

        /// <summary>
        /// Finds the clubs in the specifed region.
        /// </summary>
        /// <param name="regionId">The region Id.</param>
        /// <returns>The clubs in the specified country.</returns>
        [RpcMethod(
            "FindByRegion",
            RpcTypeCode.Array,
            Summary = "Finds the clubs in the specifed region.",
            Flags = RpcMethodFlags.Idempotent)]
        public IList<Club> FindByRegion(int regionId) {

            return ClubRepository.FindByRegion(regionId);
        }

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Get the club repository.
        /// </summary>
        protected IClubRepository ClubRepository {

            get {
                if(_clubRepository == null)
                    _clubRepository = IoC.Resolve<IClubRepository>();
                return _clubRepository;
            }
        }

        #endregion
    }
}
