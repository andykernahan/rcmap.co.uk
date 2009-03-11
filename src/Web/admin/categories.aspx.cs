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
using System.Web.UI.WebControls;
using RcMap.Model;
using RcMap.Utility;
using RcMap.Web.UI;

public partial class Admin_Categories : RcMapPage
{
    private const string CMD_INSERT = "Insert";
    private const string CMD_UPDATE = "Update";
    private const string EDIT_ROW_KEY = "Categories.EditRow";

    protected void Page_Load(object sender, EventArgs e) {

        if(IsPostBack)
            return;

        LoadInsertMode();
        Title = string.Format("{0} Category Listing", QsType);
    }
    
    private void BindCategories() {

        Categories.DataSource = CategoryRepository.FindByType(QsType);
        Categories.DataBind();        
    }    

    private void LoadInsertMode() {

        Input.Text = SortOrder.Text = string.Empty;
        Action.CommandName = CMD_INSERT;
        Action.Text = "Add";
        BindCategories();
    } 

    protected void Action_Click(object sender, EventArgs e) {

        if(!IsValid)
            return;

        Category cat;

        if(Action.CommandName == CMD_INSERT) {
            cat = new Category(QsType, Input.Text, DataUtility.ParseInt(SortOrder.Text, 0));
            CategoryRepository.Save(cat);
        } else {
            cat = CategoryRepository.FindById((int)Categories.DataKeys[(int)ViewState[EDIT_ROW_KEY]].Value);
            cat.Name = Input.Text;
            cat.SortOrder = DataUtility.ParseInt(SortOrder.Text, 0);
            CategoryRepository.Update(cat);
            ViewState.Remove(EDIT_ROW_KEY);
        }
        LoadInsertMode();
    }

    protected void Categories_RowEditing(object sender, GridViewEditEventArgs e) {

        Category cat = CategoryRepository.FindById((int)Categories.DataKeys[e.NewEditIndex].Value);

        Input.Text = cat.Name;
        SortOrder.Text = cat.SortOrder.ToString();
        Action.CommandName = CMD_UPDATE;
        Action.Text = "Save";
        ViewState[EDIT_ROW_KEY] = e.NewEditIndex;
    }

    protected void Categories_RowDeleting(object sender, GridViewDeleteEventArgs e) {

        Category cat = CategoryRepository.FindById((int)Categories.DataKeys[e.RowIndex].Value);

        CategoryRepository.Delete(cat);
        LoadInsertMode();
    }

    protected string QsType {

        get { return QueryString["type"]; }
    }
}