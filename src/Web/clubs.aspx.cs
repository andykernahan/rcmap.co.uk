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
using System.Web.UI;
using RcMap.Model;
using RcMap.Utility;
using RcMap.Web.UI;
using RcMap.Web.Utility;

public partial class ClubList : RcMapPage
{
    protected void WriteClubs(HtmlTextWriter output) {

        Region currentRegion = null;
        IList<Club> clubs = ClubRepository.CreateQuery(
            "from Club c order by c.Address.Region.Name, c.Name"
        ).List<Club>();

        output.RenderBeginTag(HtmlTextWriterTag.Dl);
        foreach(Club club in clubs) {
            if(club.Address.Region != currentRegion) {
                if(currentRegion != null) {
                    // Render the end of the DD and UL tags.
                    output.RenderEndTag();
                    output.RenderEndTag();
                }
                currentRegion = club.Address.Region;
                output.RenderBeginTag(HtmlTextWriterTag.Dt);
                output.AddAttribute(HtmlTextWriterAttribute.Href,
                    ResolveClientUrl("~" + UrlUtility.For(currentRegion)));
                output.AddAttribute(HtmlTextWriterAttribute.Title, string.Format(
                    "View RC clubs in the {0} area on the map", currentRegion.Name));
                output.RenderBeginTag(HtmlTextWriterTag.A);
                output.WriteEncodedText(currentRegion.Name);
                output.RenderEndTag();
                output.RenderEndTag();
                output.RenderBeginTag(HtmlTextWriterTag.Dd);
                output.RenderBeginTag(HtmlTextWriterTag.Ul);
            }
            output.RenderBeginTag(HtmlTextWriterTag.Li);
            output.AddAttribute(HtmlTextWriterAttribute.Href, ResolveClientUrl("~" + UrlUtility.For(club)));
            output.AddAttribute(HtmlTextWriterAttribute.Title, string.Format(
                "Go to {0} on the map", club.Name));
            output.RenderBeginTag(HtmlTextWriterTag.A);
            output.WriteEncodedText(club.Name);
            output.RenderEndTag();
            output.RenderEndTag();
        }
        // Render the end of the DD and UL tags.
        output.RenderEndTag();
        output.RenderEndTag();
        // Render the end of the DL list.
        output.RenderEndTag();
    }
}