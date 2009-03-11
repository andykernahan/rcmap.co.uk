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
using System.Collections;
using System.Web.UI.WebControls;
using RcMap.Model;
using RcMap.Utility;
using RcMap.Web.UI;

public partial class Contact : RcMapPage
{
    protected void Page_Load(object sender, EventArgs e) {

        if(IsPostBack)
            return;

        BindCategory();
        Name.Focus();
    }

    private void BindCategory() {

        ListItem item;

        Category.DataSource = CategoryRepository.FindByType(Query.CategoryType);
        Category.DataBind();
        if((item = Category.Items.FindByValue(QsCategoryId)) != null) {
            Category.ClearSelection();
            item.Selected = true;
        }
    }

    protected void SumbitButton_Click(object sender, EventArgs e) {

        if(IsValid) {
            SaveQuery();
            SendQueryMail();            
            Response.Redirect("~/submitted.aspx?type=query");
        }
    }

    private void SaveQuery() {

        Query q = new Query();

        q.UserName = Name.Text;
        q.UserEmail = Email.Text;
        q.Text = Content.Text;
        q.CreatedOn = Clock.GetTime();        
        q.HostAddress = Request.UserHostAddress;
        q.Category = CategoryRepository.FindById(DataUtility.ParseInt(Category.SelectedValue));

        QueryRepository.Save(q);
    }

    private void SendQueryMail() {

        MailDefinition def = new MailDefinition();

        def.IsBodyHtml = true;
        def.BodyFileName = "~/mail-templates/submit-query.html";
        def.Subject = "RC Map - Query Submitted";
        MailUtility.Send(def.CreateMailMessage(Configuration.EmailsTo, GetTemplateReplacements(), this));
    }

    private IDictionary GetTemplateReplacements() {

        Hashtable replacements = new Hashtable();

        replacements.Add("<%Name%>", Name.Text);        
        replacements.Add("<%Email%>", Email.Text);
        replacements.Add("<%Content%>", Content.Text);
        replacements.Add("<%Category%>", Category.SelectedItem.Text);

        return replacements;
    }

    private string QsCategoryId {

        get { return QueryString["catid"]; }
    }
}
