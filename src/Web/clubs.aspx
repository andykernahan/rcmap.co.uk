<%@ Page Language="C#" MasterPageFile="~/content.master" AutoEventWireup="true" CodeFile="clubs.aspx.cs" Inherits="ClubList" Title="RC Map - RC Club Listing" EnableViewState="false"  %>
<%@ OutputCache CacheProfile="ContentPage" %>

<asp:Content ID="ClubsMetaDesc" ContentPlaceHolderID="MetaDesc" runat="server">
    <meta name="description" content="RC Map - The complete RC club listing." />
</asp:Content>
<asp:Content ID="ClibListContent" ContentPlaceHolderID="Content" Runat="Server">
    <h1>RC Club Listing</h1>    
    <p class="single">Use <a href="http://earth.google.com/" title="Google Earth">Google Earth</a>? Click <a href="/services/export-service.ashx?format=kml&amp;disposition=attachment" title="KML Export">here</a> to download a <a href="http://code.google.com/apis/kml/documentation/" title="KML Documentation">KML</a> version of this RC club list which can be directly imported into Google Earth.</p>
    <%WriteClubs(__w);%>
</asp:Content>

