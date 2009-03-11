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
using System.Text;
using System.Xml;
using AK.Common;
using RcMap.Model;
using RcMap.Utility;
using RcMap.Web.Utility;

namespace RcMap.Web
{
    /// <summary>
    /// Exports <see cref="RcMap.Model.Location"/>s in the RSS 2.0 format. This class cannot
    /// be inherited.
    /// </summary>
    [Serializable]
    public sealed class RssLocationExporter : ILocationExporter
    {
        #region Private Fields.

        // TODO these need to be made into configuration points.
        private const string ITEM_URL = "http://www.rcmap.co.uk{0}";
        private const string FEED_URL = "http://www.rcmap.co.uk/";
        private const string FEED_TITLE = "RC Map - Your Map to RC";
        private const string FEED_DESCRIPTION = "RC Map - RC Club Feed";

        #endregion

        #region Public Interface.

        /// <summary>
        /// Exports the specified locations.
        /// </summary>
        /// <param name="locations">The locations to export.</param>
        /// <param name="output">The output.</param>
        public void Export(ICollection<Location> locations, TextWriter output)
        {
            XmlDocument document = new XmlDocument();
            XmlElement rss = CreateRssElement(document);
            XmlElement channel = CreateChannelElement(document);

            document.AppendChild(rss);
            rss.AppendChild(channel);
            foreach(Location location in locations)
                channel.AppendChild(CreateItemElement(location, document));
            using(XmlWriter writer = XmlWriter.Create(output))
                document.WriteTo(writer);
        }

        /// <summary>
        /// Gets the MIME type of the ouput format.
        /// </summary>
        public string MimeType
        {
            get { return "application/rss+xml"; }
        }

        /// <summary>
        /// Gest the file extension corresponding to the output MIME type.
        /// </summary>
        public string FileExtension
        {
            get { return ".xml"; }
        }

        #endregion

        #region Private Impl.

        private static XmlElement CreateRssElement(XmlDocument document)
        {
            XmlElement rss = document.CreateElement("rss");

            rss.SetAttribute("version", "2.0");

            return rss;
        }

        private static XmlElement CreateChannelElement(XmlDocument document)
        {
            XmlElement channel = document.CreateElement("channel");

            channel.AppendChild(CreateTextElement("title", FEED_TITLE, document));
            channel.AppendChild(CreateTextElement("link", FEED_URL, document));
            channel.AppendChild(CreateTextElement("description", FEED_DESCRIPTION, document));
            channel.AppendChild(CreateTextElement("generator", typeof(RssLocationExporter).Name, document));

            return channel;
        }

        private static XmlElement CreateItemElement(Location location, XmlDocument document)
        {
            XmlElement item = document.CreateElement("item");
            string href = string.Format(ITEM_URL, UrlUtility.For(location));
            XmlElement guid = CreateTextElement("guid", href, document);

            item.AppendChild(CreateTextElement("title", location.Name, document));
            item.AppendChild(CreateTextElement("link", href, document));
            item.AppendChild(CreateDescriptionElement(location, document));
            foreach(XmlElement category in CreateCategoryElements(location, document))
                item.AppendChild(category);
            item.AppendChild(CreateTextElement("pubDate",
                DataUtility.ToString(location.CreatedOn, "r"), document));
            guid.SetAttribute("isPermaLink", "true");
            item.AppendChild(guid);

            return item;
        }

        private static XmlElement CreateDescriptionElement(Location location, XmlDocument document)
        {
            XmlElement root = document.CreateElement("description");

            root.AppendChild(document.CreateCDataSection(GetDescription(location)));

            return root;
        }

        private static IEnumerable<XmlElement> CreateCategoryElements(Location location, XmlDocument document)
        {
            ICategorised categorised = location as ICategorised;

            if(categorised != null)
            {
                string parent = location.GetType().Name;

                foreach(Category category in categorised.Categories)
                {
                    yield return CreateTextElement("category", string.Format("{0}/{1}", parent,
                        category.Name), document);
                }
            }
        }

        private static string GetDescription(Location location)
        {
            StringBuilder sb = new StringBuilder();

            if(location is Club)
            {
                Club club = (Club)location;

                if(!string.IsNullOrEmpty(club.SiteUrl))
                    sb.AppendFormat("<a href=\"{0}\">{0}</a>", club.SiteUrl);
                sb.Append(FormatAddress(location.Address));
                sb.Append("<p>").Append(StringHelper.Join(club.Categories)).Append("</p>");
            }
            else if(location is Shop)
            {
                Shop shop = (Shop)location;

                if(!string.IsNullOrEmpty(shop.SiteUrl))
                    sb.AppendFormat("<a href=\"{0}\">{0}</a>", shop.SiteUrl);
                sb.Append(FormatAddress(location.Address));
                sb.Append("<p>").Append(StringHelper.Join(shop.Categories)).Append("</p>");
            }
            else
            {
                sb.Append(FormatAddress(location.Address));
            }

            return sb.ToString();
        }

        private static string FormatAddress(Address address)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<p class=\"adr\">");
            if(!string.IsNullOrEmpty(address.Extended))
                sb.AppendFormat("<span class=\"extended-address\">{0}</span>", address.Extended).Append(",<br />");
            sb.AppendFormat("<span class=\"street-address\">{0}</span>", address.Street).Append(",<br />");
            sb.AppendFormat("<span class=\"locality\">{0}</span>", address.Locality).Append(",<br />");
            sb.AppendFormat("<span class=\"region\">{0}</span>", address.Region.Name).Append(",<br />");
            sb.AppendFormat("<span class=\"postal-code\">{0}</span>", address.Postcode);
            sb.Append("</p>");

            return sb.ToString();
        }

        private static XmlElement CreateTextElement(string name, string value,
            XmlDocument document)
        {
            XmlElement element = document.CreateElement(name);

            if(!string.IsNullOrEmpty(value))
                element.AppendChild(document.CreateTextNode(value));

            return element;
        }

        #endregion
    }
}
