<%@ Page Language="C#" MasterPageFile="~/content.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Errors_Default" Title="RC Map Error" EnableViewState="false" %>
<%@ OutputCache CacheProfile="ContentPage" %>

<asp:Content ID="ErrorContent" ContentPlaceHolderID="Content" Runat="Server">
    <h1>Opps!</h1>
    <p class="first">Unfortunately the page you are looking for cannot be displayed at this moment in time.</p>
    <p>We apologise for any inconvenience this may have caused.</p>    
    <p><a href="/" title="Go to home">Go to home</a></p>
</asp:Content>
<asp:Content ID="ErrorLeftMenuItems" ContentPlaceHolderID="LeftMenuItems" Runat="Server" Visible="false" />
