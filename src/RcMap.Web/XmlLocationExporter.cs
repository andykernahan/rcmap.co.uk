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
using System.Xml;
using RcMap.Model;
using RcMap.Utility;

namespace RcMap.Web
{
    /// <summary>
    /// Exports <see cref="RcMap.Model.Location"/>s in the XML format. This class cannot
    /// be inherited.
    /// </summary>
    [Serializable]
    public sealed class XmlLocationExporter : ILocationExporter
    {
        #region Public Interface.

        /// <summary>
        /// Exports the specified locations.
        /// </summary>
        /// <param name="locations">The locations to export.</param>
        /// <param name="output">The output.</param>
        public void Export(ICollection<Location> locations, TextWriter output) {

            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("Locations");

            document.AppendChild(root);
            foreach(Location location in locations)
                root.AppendChild(CreateLocationElement(location, document));
            using(XmlWriter writer = XmlWriter.Create(output))
                document.WriteTo(writer);
        }

        /// <summary>
        /// Gets the MIME type of the ouput format.
        /// </summary>
        public string MimeType {

            get { return "application/xml"; }
        }

        /// <summary>
        /// Gest the file extension corresponding to the output MIME type.
        /// </summary>
        public string FileExtension {

            get { return ".xml"; }
        }

        #endregion

        #region Private Impl.

        private static XmlElement CreateLocationElement(Location location, XmlDocument document) {

            XmlElement root = document.CreateElement(location.GetType().Name);

            root.SetAttribute("Id", DataUtility.ToString(location.Id));
            root.AppendChild(CreateTextElement("Name", location.Name, document));
            root.AppendChild(CreateAddressElement(location.Address, document));
            root.AppendChild(CreatePointElement(location.GeoPoint, document));
            if(location is Club) {
                AddClubSpecificElements((Club)location, root, document);
            }

            return root;
        }

        private static void AddClubSpecificElements(Club club, XmlElement root, XmlDocument document) {

            root.AppendChild(CreateTextElement("SiteUrl", club.SiteUrl, document));
            root.AppendChild(CreateCategoriesElement(club.Categories, document));
        }

        private static XmlElement CreateAddressElement(Address address, XmlDocument document) {

            XmlElement root = document.CreateElement("Address");

            root.AppendChild(CreateTextElement("Extended", address.Extended, document));
            root.AppendChild(CreateTextElement("Street", address.Street, document));
            root.AppendChild(CreateTextElement("Locality", address.Locality, document));
            root.AppendChild(CreateNamedEntityElement("Region", address.Region, document));
            root.AppendChild(CreateTextElement("Postcode", address.Postcode, document));
            root.AppendChild(CreateNamedEntityElement("County", address.Region.Country, document));

            return root;
        }

        private static XmlElement CreatePointElement(GeoPoint point, XmlDocument document) {

            XmlElement root = document.CreateElement("Point");

            root.AppendChild(CreateTextElement("Latitude",
                DataUtility.ToString(point.Latitude), document));
            root.AppendChild(CreateTextElement("Longitude",
                DataUtility.ToString(point.Longitude), document));

            return root;
        }

        private static XmlElement CreateCategoriesElement(IEnumerable<Category> categories,
            XmlDocument document) {

            XmlElement child;
            XmlElement root = document.CreateElement("Categories");

            foreach(Category category in categories) {
                child = CreateTextElement("Category", category.Name, document);
                child.SetAttribute("Id", DataUtility.ToString(category.Id));
                root.AppendChild(child);
            }

            return root;
        }

        private static XmlElement CreateNamedEntityElement(string name, INamedEntity entity, XmlDocument document) {

            XmlElement root = CreateTextElement(name, entity.Name, document);

            root.SetAttribute("Id", DataUtility.ToString(entity.Id));

            return root;
        }

        private static XmlElement CreateTextElement(string name, string value,
            XmlDocument document) {

            XmlElement element = document.CreateElement(name);

            if(!string.IsNullOrEmpty(value))
                element.AppendChild(document.CreateTextNode(value));

            return element;
        }

        #endregion
    }
}
