<%@ Page Language="C#" MasterPageFile="~/content.master" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="Errors_404" Title="RC Map - Page Not Found" EnableViewState="false" %>
<%@ OutputCache CacheProfile="ContentPage" %>

<asp:Content ID="ErrorContent" ContentPlaceHolderID="Content" Runat="Server">
    <h1>Opps! Page not found!</h1>
    <p class="first">Unfortunately the page you are looking for cannot be found, this may be caused by the following:</p>
    <ul>
        <li>If a search engine link brought you to this page, the page may no longer exist or it may have been moved. If so, please go to the <a href="/" title="Go to home">home</a> page and try to find the page you are looking for</li>                
        <li>If a link on this site brought you to this page, please let us <asp:HyperLink ID="ReportLink" runat="server" ToolTip="Report link" NavigateUrl="mailto:andy@rcmap.co.uk?subject={0} is dead" Text="know" /> so we may fix it</li>
        <li>If you have typed the link into your browser, please ensure that it is correct and try again</li>
    </ul>
    <p>We apologise for any inconvenience this may have caused.</p>    
    <p><a href="/" title="Go to home">Go to home</a></p>
</asp:Content>
<asp:Content ID="ErrorLeftMenuItems" ContentPlaceHolderID="LeftMenuItems" Runat="Server" Visible="false" />

