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
using System.Web;
using System.Collections.Generic;
using System.Text;
using AK.Common;
using AK.Net.Json;
using AK.Net.Json.Conversion;
using RcMap.Model;
using RcMap.Utility;
using RcMap.Web.UI;
using RcMap.Web.Utility;

public partial class _Default : RcMapPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(MoveLegacyLinks())
        {
            return;
        }
        RegisterScripts();
        if(RedirectExplicitIds())
        {

        }
        BindCategories();
        ConfigureAnalytics();        
    }

<<<<<<< .mine
    private bool RedirectExplicitIds()
    {
        if(!string.IsNullOrEmpty(ClubId))
        {
            Club club = this.ClubRepository.FindById(DataUtility.ParseInt(ClubId));
            if(club != null)
            {
                Response.Status = "301 Moved Permanently";
                Response.Redirect("/club/" + club.Name);
            }
        }
        else if(!string.IsNullOrEmpty(RegionId))
        {
        }
        return false;
    }

    private void ConfigureAnalytics() {

        this.Analytics.Visible = this.Configuration.EnableAnalytics;
=======
    private void ConfigureAnalytics()
    {
        Analytics.Visible = Configuration.EnableAnalytics;
>>>>>>> .r824
    }

    private void BindCategories()
    {
        ClubCategoryOptions.DataSource = CategoryRepository.FindByType(Club.CategoryType);
        ClubCategoryOptions.DataBind();
    }

    private void RegisterScripts()
    {
        Club club;
        Region region;
        if(TryLoadClub(out club))
        {
            ClientScript.RegisterClientScriptBlock(typeof(_Default), "_initialClubs", string.Format(
                "<script type=\"text/javascript\">//<![CDATA[\r\nvar _initialClubs = {0};\r\n//]]></script>",
                JsonEncodeInitialClubs(club)));
            UpdateMetaDescAndWindowTitle(club);
<<<<<<< .mine
        } else if(TryLoadRegion(out region)) {
            IList<Club> clubsInRegion = this.ClubRepository.FindByRegion(region.Id);
            this.ClientScript.RegisterClientScriptBlock(typeof(_Default), "_initialClubs", string.Format(
=======
        }
        else if(TryLoadRegion(out region))
        {
            IList<Club> clubsInRegion = ClubRepository.FindByRegion(region.Id);
            ClientScript.RegisterClientScriptBlock(typeof(_Default), "_initialClubs", string.Format(
>>>>>>> .r824
                "<script type=\"text/javascript\">//<![CDATA[\r\nvar _initialClubs = {0};\r\n//]]></script>",
                JsonEncodeInitialClubs(clubsInRegion)));
            UpdateMetaDescAndWindowTitle(region, clubsInRegion);
        }
        else
        {
            // The form is required by the script manager only.
            aspnetForm.Visible = false;
        }
    }

    private bool TryLoadClub(out Club club)
    {
        club = null;
<<<<<<< .mine
        if(!string.IsNullOrEmpty(this.ClubId) && (id = DataUtility.ParseInt(this.ClubId, -1)) != -1)
            club = this.ClubRepository.FindById(id);

        return club != null;
=======
        if(!"club".Equals(LoadType, StringComparison.Ordinal))
        {
            return false;
        }
        if((club = ClubRepository.FindBySlug(LoadSlug)) == null)
        {
            throw Make404("Invalid club name.");
        }
        return true;
>>>>>>> .r824
    }

    private bool TryLoadRegion(out Region region)
    {
        region = null;
<<<<<<< .mine
        if(!string.IsNullOrEmpty(this.RegionId) && (id = DataUtility.ParseInt(this.RegionId, -1)) != -1)
            region = this.RegionRepository.FindById(id);

        return region != null;
=======
        if(!"region".Equals(LoadType, StringComparison.Ordinal))
        {
            return false;
        }
        if((region = RegionRepository.FindBySlug(LoadSlug)) == null)
        {
            throw Make404("Invalid region name.");
        }
        return true;
>>>>>>> .r824
    }

    private static string JsonEncodeInitialClubs(Club club)
    {
        return JsonEncodeInitialClubs(new Club[] { club });
    }

    private static string JsonEncodeInitialClubs(IEnumerable<Club> clubs)
    {
        JsonArray containers = new JsonArray();

        foreach(Club club in clubs)
            containers.Add(JsonSerializer.Serialize(club));

        using(JsonWriter writer = new JsonWriter())
        {
            writer.Write(containers);
            return writer.ToString();
        }
    }

    private void UpdateMetaDescAndWindowTitle(Region region, IList<Club> clubsInRegion)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("RC Map - ");
        sb.Append("Find RC clubs near you in the ").Append(region.Name).Append(" area!");

        windowTitle.InnerText = sb.ToString();

        if(clubsInRegion.Count == 0)
            return;

        Shuffle(clubsInRegion);

        sb.Append(" Such as ");
        sb.Append(GetShortDescription(clubsInRegion[0]));
        if(clubsInRegion.Count > 1)
            sb.Append(" and ").Append(GetShortDescription(clubsInRegion[1]));
        sb.Append("!");

        metaDescription.Attributes["content"] = sb.ToString();
    }

    private void Shuffle<T>(IList<T> collection)
    {
        int j;
        T temp;
        Random random = new Random();

        for(int i = collection.Count; --i > 0; )
        {
            j = random.Next(i);
            temp = collection[i];
            collection[i] = collection[j];
            collection[j] = temp;
        }
    }

    private static string GetShortDescription(Club club)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(club.Name);
        sb.Append(" (").Append(StringHelper.Join(club.Categories)).Append(")");

        return sb.ToString();
    }

    private void UpdateMetaDescAndWindowTitle(Club club)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("RC Map - ");
        sb.Append(club.Name).Append(", ");
        if(!string.IsNullOrEmpty(club.Address.Extended))
            sb.Append(club.Address.Extended).Append(", ");
        if(!string.IsNullOrEmpty(club.Address.Street))
            sb.Append(club.Address.Street).Append(", ");
        if(!string.IsNullOrEmpty(club.Address.Locality))
            sb.Append(club.Address.Locality).Append(", ");
        sb.Append(club.Address.Region.Name).Append(", ");
        sb.Append(StringHelper.Join(club.Categories));
        metaDescription.Attributes["content"] = windowTitle.InnerText = sb.ToString();
    }

<<<<<<< .mine
    private string ClubId {
=======
    private bool MoveLegacyLinks()
    {
        string url = null;
        if(!string.IsNullOrEmpty(LegacyLoadClubId))
        {
            url = UrlUtility.For(LegacyLoadClub());
        }
        if(!string.IsNullOrEmpty(LegacyLoadRegionId))
        {
            url = UrlUtility.For(LegacyOldRegion());
        }
        if(url != null)
        {
            Response.Clear();
            Response.StatusCode = 301;
            Response.AddHeader("Location", url);
            Response.End();
            return true;
        }
        return false;
    }
>>>>>>> .r824

    private Club LegacyLoadClub()
    {
        Club club;
        int id = DataUtility.ParseInt(LegacyLoadClubId, -1);
        if(id == -1 || (club = ClubRepository.FindById(id)) == null)
        {
            throw Make404("Invalid club Id.");
        }
        return club;
    }

<<<<<<< .mine
    private string ClubName
    {
        get { return this.QueryString["loc"]; }
    }
=======
    private Region LegacyOldRegion()
    {
        Region region;
        int id = DataUtility.ParseInt(LegacyLoadRegionId, -1);
        if(id == -1 || (region = RegionRepository.FindById(id)) == null)
        {
            throw Make404("Invalid region Id.");
        }
        return region;
    }
>>>>>>> .r824

<<<<<<< .mine
    private string RegionId {

        get { return this.QueryString["regionid"]; }
=======
    private static HttpException Make404(string message)
    {
        return new HttpException(404, message);
>>>>>>> .r824
    }
<<<<<<< .mine

    private string RegionName
    {
        get { return this.QueryString["region"]; }
    }
=======

    private string LoadType
    {
        get { return QueryString["type"]; }
    }

    private string LoadSlug
    {
        get { return QueryString["slug"]; }
    }

    private string LegacyLoadClubId
    {
        get { return QueryString["locid"]; }
    }

    private string LegacyLoadRegionId
    {
        get { return QueryString["regionid"]; }
    }
>>>>>>> .r824
}