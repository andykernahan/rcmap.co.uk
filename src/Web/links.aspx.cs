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
using RcMap.Web.UI;

public partial class LinkList : RcMapPage {

    protected void WriteLinks(HtmlTextWriter output) {

        Category currentCategory = null;
        IList<Link> links = LinkRepository.CreateQuery(
            "from Link l order by l.Category.Name, l.Name"
        ).List<Link>();
        
        output.RenderBeginTag(HtmlTextWriterTag.Dl);
        foreach(Link link in links) {
            if(link.Category != currentCategory) {
                if(currentCategory != null) {
                    // Render the end of the DD and UL tags.
                    output.RenderEndTag();
                    output.RenderEndTag();
                }
                currentCategory = link.Category;
                output.RenderBeginTag(HtmlTextWriterTag.Dt);
                output.WriteEncodedText(currentCategory.Name);
                output.RenderEndTag();
                output.RenderBeginTag(HtmlTextWriterTag.Dd);
                output.RenderBeginTag(HtmlTextWriterTag.Ul);
            }
            output.RenderBeginTag(HtmlTextWriterTag.Li);
            output.AddAttribute(HtmlTextWriterAttribute.Href, link.Url);            
            output.AddAttribute(HtmlTextWriterAttribute.Target, "_blank");
            output.AddAttribute(HtmlTextWriterAttribute.Title, string.Format("Open {0} in a new window", link.Url));
            output.RenderBeginTag(HtmlTextWriterTag.A);
            output.WriteEncodedText(link.Name);
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