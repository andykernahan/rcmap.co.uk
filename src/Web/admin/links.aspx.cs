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
using RcMap.Model;
using RcMap.Utility;
using RcMap.Web.UI;

public partial class Admin_Links : RcMapPage
{
    protected PagerHelper _pager;

    protected void Page_Load(object sender, EventArgs e) {

        _pager = new PagerHelper(GetCurrentPage(), GetPageSize(), LinkRepository.GetCount());
        Links.DataSource = LinkRepository.CreateQuery(
            "from Link l order by l.Category.Name, l.Name"
        ).SetFirstResult(_pager.CurrentRow).SetMaxResults(_pager.PageSize).List();
        Links.DataBind();        
    }

    private int GetCurrentPage() {

        return Math.Max(DataUtility.ParseInt(QueryString["page"], 0), 0);
    }

    private int GetPageSize() {

        return Math.Max(DataUtility.ParseInt(QueryString["size"], 20), 1);
    } 

    protected string GetDescription(Link link) {

        const int maxLength = 65;

        return link.Description.Length > maxLength ?
            link.Description.Substring(0, maxLength) + "..." :
            link.Description;
    }
}
