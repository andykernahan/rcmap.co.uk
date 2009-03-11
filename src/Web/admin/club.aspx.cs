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

public partial class Admin_Club : RcMapPage
{
    private const string CMD_INSERT = "Insert";
    private const string CMD_UPDATE = "Update";

    protected void Page_Load(object sender, EventArgs e) {

        if(IsPostBack)
            return;

        BindRegions();
        BindClubCategories();
        if(!string.IsNullOrEmpty(QsClubId))
            LoadEditMode();
        else
            LoadInsertMode();
    }

    private void LoadEditMode() {

        PopulateControlsFromClub(ClubRepository.FindById(DataUtility.ParseInt(QsClubId)));
        Submit.CommandName = CMD_UPDATE;
        Submit.Text = "Save";
    }

    private void LoadInsertMode() {

        Submit.CommandName = CMD_INSERT;
        Submit.Text = "Add";
        ClubTitle.Text = "New Club";
        Delete.Visible = DeleteSep.Visible = false;        
    }

    protected void Submit_Click(object sender, EventArgs e) {

        if(!IsValid)
            return;

        Club club;

        if(Submit.CommandName == CMD_UPDATE) {
            club = ClubRepository.FindById(DataUtility.ParseInt(QsClubId));
            PopulateClubFromControls(club);
            ClubRepository.Update(club);
        } else if(Submit.CommandName == CMD_INSERT) {
            club = new Club();
            PopulateClubFromControls(club);
            ClubRepository.Save(club);
        }
        Response.Redirect(Cancel.NavigateUrl);
    }

    protected void Delete_Click(object sender, EventArgs e) {

        Club club = ClubRepository.FindById(DataUtility.ParseInt(QsClubId));

        ClubRepository.Delete(club);
        Response.Redirect(Cancel.NavigateUrl);
    }

    private void BindRegions() {
            
        Region.DataSource = RegionRepository.CreateQuery(
            "from Region r where r.Country.Name = 'United Kingdom' order by r.Name"
        ).List();
        Region.DataBind();
    }

    private void BindClubCategories() {

        ClubCategories.DataSource = CategoryRepository.FindByType(Club.CategoryType);
        ClubCategories.DataBind();
    }

    private void PopulateControlsFromClub(Club club) {

        Title = ClubTitle.Text = Name.Text = club.Name;
        Extended.Text = club.Address.Extended;
        Street.Text = club.Address.Street;
        Locality.Text = club.Address.Locality;
        Region.Items.FindByValue(DataUtility.ToString(club.Address.Region.Id)).Selected = true;
        Postcode.Text = club.Address.Postcode;
        Latitude.Text = club.GeoPoint.Latitude.ToString();
        Longitude.Text = club.GeoPoint.Longitude.ToString();        
        if(club.Contact != null) {
            ContactName.Text = club.Contact.Name;
            ContactEmail.Text = club.Contact.Email;
        }
        SiteUrl.Text = club.SiteUrl;
        foreach(Category cat in club.Categories)
            ClubCategories.Items.FindByValue(DataUtility.ToString(cat.Id)).Selected = true;
        Audit.Text = string.Format("created on {0} by {1} modified on {2} by {3}",
            club.CreatedOn.ToString("d"), club.CreatedBy, club.ModifiedOn.ToString("d"), club.ModifiedBy);
    }

    private void PopulateClubFromControls(Club club) {        

        club.Name = Name.Text;
        if(club.Address == null)
            club.Address = new Address();
        club.Address.Extended = Extended.Text;
        club.Address.Street = Street.Text;
        club.Address.Locality = Locality.Text;
        club.Address.Region = RegionRepository.FindById(DataUtility.ParseInt(Region.SelectedItem.Value));        
        club.Address.Postcode = Postcode.Text;
        if(club.GeoPoint == null)
            club.GeoPoint = new GeoPoint();
        club.GeoPoint.Latitude = double.Parse(Latitude.Text);
        club.GeoPoint.Longitude = double.Parse(Longitude.Text);        
        club.Contact = PopulateContactFromControls(club.Contact);
        club.SiteUrl = SiteUrl.Text;
        club.Categories.Clear();
        foreach(ListItem item in ClubCategories.Items) {
            if(item.Selected)
                club.Categories.Add(CategoryRepository.FindById(DataUtility.ParseInt(item.Value)));
        }
    }

    private Contact PopulateContactFromControls(Contact contact) {
        
        string name = ContactName.Text.Trim();
        string email = ContactEmail.Text.Trim();

        if(!name.Equals(string.Empty)) {
            if(contact == null)
                contact = new Contact();            
        }
        if(contact != null) {
            contact.Name = name;
            contact.Email = email;
        }

        return contact;
    }

    private string QsClubId {

        get { return QueryString["clubid"]; }
    }
}

