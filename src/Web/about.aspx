<%@ Page Language="C#" MasterPageFile="~/content.master" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="About" Title="RC Map - About RC Map" EnableViewState="false" %>
<%@ OutputCache CacheProfile="ContentPage" %>

<asp:Content ID="AboutMetaDesc" ContentPlaceHolderID="MetaDesc" runat="server">
    <meta name="description" content="RC Map - About RC Map" />
</asp:Content>
<asp:Content ID="AboutContent" ContentPlaceHolderID="Content" Runat="Server">
    <h1>About RC Map</h1>
    <p class="first">RC Map is designed to help you find RC clubs in your area easily and we hope you find it useful.</p>
    <p>If you would like to leave some feedback, suggest a feature, or just have a general question regarding the site, please feel free to <a href="/contact.aspx" title="Contact Us">contact</a> us.</p>
    <p>If you know of a club that is not on the map, please <a href="/submit.aspx" title="Submit a club">submit</a> it and we will add it to the map!</p>
    <p class="sign-off">RC Map</p>
</asp:Content>

