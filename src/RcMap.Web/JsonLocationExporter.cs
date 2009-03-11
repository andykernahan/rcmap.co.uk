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
using System.IO;
using AK.Net.Json;
using AK.Net.Json.Conversion;
using RcMap.Model;

namespace RcMap.Web
{
    /// <summary>
    /// Exports <see cref="RcMap.Model.Location"/>s in the JSON format. This class cannot
    /// be inherited.
    /// </summary>
    [Serializable]
    public sealed class JsonLocationExporter : ILocationExporter
    {
        #region Public Interface.

        /// <summary>
        /// Exports the specified locations.
        /// </summary>
        /// <param name="locations">The locations to export.</param>
        /// <param name="output">The output.</param>
        public void Export(ICollection<Location> locations, TextWriter output) {

            using(JsonWriter writer = new JsonWriter(output, false))
                writer.Write(JsonSerializer.Serialize(locations));
        }

        /// <summary>
        /// Gets the MIME type of the ouput format.
        /// </summary>
        public string MimeType {

            get { return "application/json"; }
        }

        /// <summary>
        /// Gest the file extension corresponding to the output MIME type.
        /// </summary>
        public string FileExtension {

            get { return ".json"; }
        }

        #endregion
    }
}
