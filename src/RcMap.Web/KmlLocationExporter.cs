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
    /// Exports <see cref="RcMap.Model.Location"/>s in the KML format. This class cannot
    /// be inherited.
    /// </summary>
    [Serializable]
    public sealed class KmlLocationExporter : ILocationExporter
    {
        #region Private Fields.

        private const string KML_NS = "http://earth.google.com/kml/2.2";
        private const string ATOM_NS = "http://www.w3.org/2005/Atom";
        private const string ATOM_PREFIX = "atom";
        private const string ATOM_XMLNS = "xmlns:" + ATOM_PREFIX;

        // TODO these need to be made into configuration points.
        private const string ITEM_LINK = "http://www.rcmap.co.uk/default.aspx?locid={0}";
        private const string FEED_URL = "http://www.rcmap.co.uk/";
        private const string FEED_TITLE = "RC Map - Your Map to RC";
        private const string FEED_AUTHOR = "RC Map";

        #endregion

        #region Public Interface.

        /// <summary>
        /// Exports the specified locations.
        /// </summary>
        /// <param name="locations">The locations to export.</param>
        /// <param name="output">The output.</param>
        public void Export(ICollection<Location> locations, TextWriter output) {

            XmlDocument document = new XmlDocument();
            XmlElement kml = CreateElement("kml", document);
            XmlElement doc = CreateElement("Document", document);

            document.AppendChild(kml);
            kml.AppendChild(doc);
            kml.Attributes.Append(CreateAtomXmlnsAttribute(document));
            AddAttributionElements(doc, document);
            foreach(Location location in locations)
                doc.AppendChild(CreatePlacemarkElement(location, document));
            using(XmlWriter writer = XmlWriter.Create(output))
                document.WriteTo(writer);
        }

        /// <summary>
        /// Gets the MIME type of the ouput format.
        /// </summary>
        public string MimeType {

            get { return "application/vnd.google-earth.kml+xml"; }
        }

        /// <summary>
        /// Gest the file extension corresponding to the output MIME type.
        /// </summary>
        public string FileExtension {

            get { return ".kml"; }
        }

        #endregion

        #region Private Impl.

        private static XmlAttribute CreateAtomXmlnsAttribute(XmlDocument document) {

            XmlAttribute attr = document.CreateAttribute(ATOM_XMLNS);

            attr.Value = ATOM_NS;

            return attr;
        }

        private void AddAttributionElements(XmlElement kml, XmlDocument document) {

            kml.AppendChild(CreateTextElement("name", FEED_TITLE, document));
            kml.AppendChild(CreateAtomAuthorElement(document));
            kml.AppendChild(CreateAtomLinkElement(document));
        }

        private XmlElement CreateAtomAuthorElement(XmlDocument document) {

            XmlElement root = CreateElement("author", Ns.Atom, document);

            root.AppendChild(CreateTextElement("name", FEED_AUTHOR, Ns.Atom, document));

            return root;
        }

        private static XmlElement CreateAtomLinkElement(XmlDocument document) {

            XmlElement root = CreateElement("link", Ns.Atom, document);
            XmlAttribute href = document.CreateAttribute(ATOM_PREFIX, "href", ATOM_NS);

            href.Value = FEED_URL;
            root.Attributes.Append(href);

            return root;
        }

        private static XmlElement CreatePlacemarkElement(Location location, XmlDocument document) {

            XmlElement root = CreateElement("Placemark", document);

            root.AppendChild(CreateTextElement("name", location.Name, document));
            root.AppendChild(CreatePointElement(location.GeoPoint, document));
            root.AppendChild(CreateAddressElement(location.Address, document));
            if(location is Club) {
                root.AppendChild(CreateDescriptionElement(location, document));
            }
            // This has been removed as it prevents placemarks from being displayed by
            // default int GoogleEarth. In order to display the elements, the time display
            // must be set to none. 
            //root.AppendChild(CreateTimeStampElement(location, document));

            return root;
        }

        private static XmlElement CreateTimeStampElement(Location location, XmlDocument document) {

            XmlElement root = CreateElement("TimeStamp", document);

            root.AppendChild(CreateTextElement(
                "when",
                location.CreatedOn.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH:mm:ss'Z'"), 
                document
            ));

            return root;
        }

        private static XmlElement CreateDescriptionElement(Location location, XmlDocument document) {

            XmlElement root = CreateElement("description", document);

            root.AppendChild(document.CreateCDataSection(FormatCategories(((Club)location).Categories)));

            return root;
        }

        private static XmlElement CreateAddressElement(Address addr, XmlDocument document) {

            return CreateTextElement("address", FormatAddress(addr), document);
        }

        private static XmlElement CreatePointElement(GeoPoint point, XmlDocument document) {

            XmlElement root = CreateElement("Point", document);

            root.AppendChild(CreateTextElement("coordinates", FormatGeoPoint(point), document));

            return root;
        }

        private static string FormatGeoPoint(GeoPoint point) {

            return string.Format("{0},{1}", DataUtility.ToString(point.Longitude),
                DataUtility.ToString(point.Latitude));
        }

        private static string FormatAddress(Address address) {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if(!string.IsNullOrEmpty(address.Extended))
                sb.Append(address.Extended).Append(", ");
            sb.Append(address.Street).Append(", ");
            sb.Append(address.Locality).Append(", ");
            sb.Append(address.Region.Name).Append(", ");
            sb.Append(address.Postcode);

            return sb.ToString();
        }

        private static string FormatCategories(IEnumerable<Category> categories) {

            return AK.Common.StringHelper.Join(categories, ", ");
        }

        private static XmlElement CreateTextElement(string name, string value, XmlDocument document) {

            return CreateTextElement(name, value, Ns.Kml, document);
        }

        private static XmlElement CreateTextElement(string name, string value, Ns ns, XmlDocument document) {

            XmlElement element = CreateElement(name, ns, document);

            if(!string.IsNullOrEmpty(value))
                element.AppendChild(document.CreateTextNode(value));

            return element;
        }

        private static XmlElement CreateElement(string name, XmlDocument document) {

            return CreateElement(name, Ns.Kml, document);
        }

        private static XmlElement CreateElement(string name, Ns ns, XmlDocument document) {

            switch(ns) {
                case Ns.Kml:
                    return document.CreateElement(name, KML_NS);
                case Ns.Atom:
                    return document.CreateElement(ATOM_PREFIX, name, ATOM_NS);
                default:
                    throw Error.Internal("Invalid Ns");
            }
        }

        private enum Ns
        {
            Kml,
            Atom
        }

        #endregion
    }
}
