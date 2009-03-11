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
using AK.Net.Json;
using AK.Net.Json.Conversion;
using AK.Common;

namespace RcMap.Model.Converters
{   
    [Serializable]
    internal sealed class LocationConverter
    {
        #region Internal Interface.

        internal static JsonObject Convert(Location location, IJsonSerializer serializer) {

            JsonObject container = new JsonObject();

            container.Add("Id", location.Id);
            container.Add("Name", location.Name);
            container.Add("Addr", serializer.Serialize(location.Address));
            container.Add("Pt", serializer.Serialize(location.GeoPoint));            

            return container;
        }

        #endregion
    } 
}
