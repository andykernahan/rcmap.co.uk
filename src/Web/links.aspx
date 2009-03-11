<%@ Page Language="C#" MasterPageFile="~/content.master" AutoEventWireup="true" CodeFile="links.aspx.cs" Inherits="LinkList" Title="RC Map - Useful RC Links" %>
<%@ OutputCache CacheProfile="ContentPage" %>

<asp:Content ID="ClubsMetaDesc" ContentPlaceHolderID="MetaDesc" runat="server">
    <meta name="description" content="RC Map - Links to other useful sites such as RC forums, shops, manufacturers etc." />
</asp:Content>
<asp:Content ID="UsefulLinksContent" ContentPlaceHolderID="Content" Runat="Server">
    <h1>Useful RC Links</h1>
    <p class="single">Know a link that others may find useful that's not here? Please <asp:HyperLink ID="SumbitLink" runat="server" NavigateUrl="contact.aspx?catid=78" Text="submit" ToolTip="Submit a link" /> it and we will add it the the list!</p>
    <%WriteLinks(__w);%>
</asp:Content>

