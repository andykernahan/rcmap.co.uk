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

public partial class Admin_Query : RcMapPage
{
    protected void Page_Load(object sender, EventArgs e) {

        Query q = QueryRepository.FindById(DataUtility.ParseInt(QueryString["queryid"]));

        PopulateControlsFromQuery(q);
    }

    protected void DeleteButton_Click(object sender, EventArgs e) {

        Query q = QueryRepository.FindById(DataUtility.ParseInt(QueryString["queryid"]));

        QueryRepository.Delete(q);

        Response.Redirect("~/admin/queries.aspx");
    }

    private void PopulateControlsFromQuery(Query q) {

        Name.Text = q.UserName;
        UserName.Text = q.UserName;
        UserEmail.Text = q.UserEmail;
        Category.Text = q.Category.Name;
        Content.Text = q.Text;
        Audit.Text = string.Format("created on {0} by host {1}", q.CreatedOn.ToString("g"), q.HostAddress);
    }
}