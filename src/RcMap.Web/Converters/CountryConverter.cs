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
    /// <summary>
    /// Provides support for converting <see cref="RcMap.Model.Country"/>s into thier
    /// equivalent JavaScript Object Notation representation. This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class CountryConverter : JsonConverter
    {
        #region Public Interface.

        /// <summary>
        /// Returns a value indicating if this converter is capable of converting the specified
        /// object.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <returns>True if the converter can convert the specified object, otherwise; false.
        /// </returns>
        public override bool CanConvert(object target) {

            return CanConvertImpl(target) || base.CanConvert(target);
        }

        /// <summary>
        /// Converts the specified object into it's equivalent JavaScript Object Notation
        /// representation.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <param name="serializer">The serializer currently controlling the serialization process.</param>
        /// <returns>The converted object.</returns>
        public override IJsonType Convert(object obj, IJsonSerializer serializer) {

            if(!CanConvertImpl(obj))
                return base.Convert(obj, serializer);            

            return Convert((Country)obj, serializer);
        }

        #endregion

        #region Internal Interface.

        internal static IJsonType Convert(Country country, IJsonSerializer serializer) {

            return new JsonString(country.Name);
        }

        #endregion

        #region Private Impl.

        private static readonly Type TARGET_TYPE = typeof(Country);

        private static bool CanConvertImpl(object target) {

            return ReflectionHelper.IsOfType(target, TARGET_TYPE);
        }

        #endregion
    } 
}
