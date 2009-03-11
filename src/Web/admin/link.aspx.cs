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

public partial class Admin_Link : RcMapPage
{
    private const string CMD_INSERT = "Insert";
    private const string CMD_UPDATE = "Update";

    protected void Page_Load(object sender, EventArgs e) {

        if(IsPostBack)
            return;
        
        BindLinkCategories();
        if(!string.IsNullOrEmpty(QsLinkId))
            LoadEditMode();
        else
            LoadInsertMode();        
    }

    private void LoadEditMode() {

        PopulateControlsFromLink(LinkRepository.FindById(DataUtility.ParseInt(QsLinkId)));
        Submit.CommandName = CMD_UPDATE;
        Submit.Text = "Save";
    }

    private void LoadInsertMode() {

        Submit.CommandName = CMD_INSERT;
        Submit.Text = "Add";
        LinkTitle.Text = "New Link";
        Delete.Visible = DeleteSep.Visible = false;
    }

    protected void Submit_Click(object sender, EventArgs e) {

        if(!IsValid)
            return;

        Link link;

        if(Submit.CommandName == CMD_UPDATE) {
            link = LinkRepository.FindById(DataUtility.ParseInt(QsLinkId));
            PopulateLinkFromControls(link);
            LinkRepository.Update(link);
        } else if(Submit.CommandName == CMD_INSERT) {
            link = new Link();
            PopulateLinkFromControls(link);
            LinkRepository.Save(link);
        }
        Response.Redirect(Cancel.NavigateUrl);
    }

    protected void Delete_Click(object sender, EventArgs e) {

        Link link = LinkRepository.FindById(DataUtility.ParseInt(QsLinkId));

        LinkRepository.Delete(link);
        Response.Redirect(Cancel.NavigateUrl);
    }

    private void BindLinkCategories() {

        Category.DataSource = CategoryRepository.FindByType(Link.CategoryType);
        Category.DataBind();
    }

    private void PopulateControlsFromLink(Link link) {

        Title = LinkTitle.Text = Name.Text = link.Name;
        Url.Text = link.Url;
        Category.ClearSelection();
        Category.Items.FindByValue(DataUtility.ToString(link.Category.Id)).Selected = true;
        Description.Text = link.Description;
    }

    private void PopulateLinkFromControls(Link link) {

        link.Name = Name.Text;
        link.Url = Url.Text;
        link.Category = CategoryRepository.FindById(DataUtility.ParseInt(Category.SelectedValue));
        link.Description = Description.Text;
    }

    private string QsLinkId {

        get { return QueryString["linkid"]; }
    }
}