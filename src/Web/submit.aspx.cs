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

public partial class SubmitClub : RcMapPage {

    protected void Page_Load(object sender, EventArgs e) {
        
        ClientScript.RegisterClientScriptInclude("core", "/js/core.js");
        ClientScript.RegisterClientScriptInclude("submit-page", "/js/submit-page.js");

        if(IsPostBack)
            return;

        BindRegion();
        BindClubCategories();                
    }

    private void BindClubCategories() {

        ListItem item;        

        ClubCategories.DataSource = CategoryRepository.FindByType(Club.CategoryType);
        ClubCategories.DataBind();
        if((item = ClubCategories.Items.FindByValue(QsCategoryId)) != null) {
            ClubCategories.ClearSelection();
            item.Selected = true;
        }
    }

    private void BindRegion() {

        // TODO this query should be factored out.
        Region.DataSource = RegionRepository.CreateQuery(
            "from Region r where r.Country.Name = 'United Kingdom' order by r.Name"
        ).List();
        Region.DataBind();
    }

    protected void SumbitButton_Click(object sender, EventArgs e) {

        if(IsValid) {
            SendSubmitMail();
            Response.Redirect("~/submitted.aspx?type=club");
        }        
    }

    private void SendSubmitMail() {

        MailDefinition def = new MailDefinition();

        def.IsBodyHtml = true;
        def.BodyFileName = "~/mail-templates/submit-club.html";
        def.Subject = "RC Map - Club Submitted";
        MailUtility.Send(def.CreateMailMessage(Configuration.EmailsTo, GetTemplateReplacements(), this));
    }

    private IDictionary GetTemplateReplacements() {

        Hashtable replacements = new Hashtable();

        replacements.Add("<%Name%>", Name.Text);
        replacements.Add("<%Extended%>", Extended.Text);
        replacements.Add("<%Street%>", Street.Text);
        replacements.Add("<%Locality%>", Locality.Text);
        replacements.Add("<%Region%>", Region.SelectedItem.Text);
        replacements.Add("<%Postcode%>", Postcode.Text);
        replacements.Add("<%Categories%>", GetSelectedClubCatsString());
        replacements.Add("<%Website%>", SiteUrl.Text);
        replacements.Add("<%ContactName%>", ContactName.Text);
        replacements.Add("<%Email%>", Email.Text);

        return replacements;
    }

    private string GetSelectedClubCatsString() {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach(ListItem item in ClubCategories.Items) {
            if(item.Selected) {
                if(sb.Length > 0)
                    sb.Append(", ");
                sb.Append(item.Text);
            }
        }

        return sb.ToString();
    }

    private string QsCategoryId {

        get { return QueryString["cat"]; }
    }
}